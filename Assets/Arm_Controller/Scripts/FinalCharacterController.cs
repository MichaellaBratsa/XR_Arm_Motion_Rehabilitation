using UnityEngine;
using System.Globalization;
using TMPro;

public class FinalCharacterController : MonoBehaviour
{
    public enum SourceAxis { X, Y, Z }

    //public TextMeshProUGUI elbowAngle, shoulderAngle;

    [System.Serializable]
    public class Bone
    {
        public Transform bone;
        public string Name = "Sensor";

        [Header("Axis Mapping & Sensitivity")]
        public SourceAxis Source_For_Pitch = SourceAxis.X;
        [Range(-2f, 2f)] public float Pitch_Multiplier = 1f;
        public SourceAxis Source_For_Yaw = SourceAxis.Y;
        [Range(-2f, 2f)] public float Yaw_Multiplier = 1f;
        public SourceAxis Source_For_Roll = SourceAxis.Z;
        [Range(-2f, 2f)] public float Roll_Multiplier = 1f;

        [Header("Smoothing")]
        [Range(1f, 50f)] public float SmoothSpeed = 20f;

        [HideInInspector] public Quaternion calibrationOffset = Quaternion.identity;
        [HideInInspector] public Quaternion initialWorldRot;

        // ΠΡΟΣΘΗΚΗ: Αποθηκεύει τον "στόχο" της περιστροφής για να γίνει το Slerp στην Update
        [HideInInspector] public Quaternion targetRotation;
    }

    public Bone Elbow; //Sensor id = 0
    public Bone Shoulder; //Sensor id = 1

    private string buffer = "";

    // ΠΡΟΣΘΗΚΗ: Thread-safe μεταβλητές για την επικοινωνία μεταξύ Java/BLE και Unity Main Thread
    private string threadSafeBuffer = "";
    private readonly object dataLock = new object();

    private bool calibrateNextFrame = false;

    void Start()
    {
        // Capture the initial pose of the character model
        if (Elbow.bone)
        {
            Elbow.initialWorldRot = Elbow.bone.rotation;
            Elbow.targetRotation = Elbow.initialWorldRot; // Αρχικοποίηση
            //elbowAngle.text = "Elbow";
        }

        if (Shoulder.bone)
        {
            Shoulder.initialWorldRot = Shoulder.bone.rotation;
            Shoulder.targetRotation = Shoulder.initialWorldRot; // Αρχικοποίηση
            //shoulderAngle.text = "Shoulder";
        }

        // Subscribe to the Bluetooth receiver's data event
        if (SimpleBleReceiver.Instance != null)
            SimpleBleReceiver.Instance.OnPacketReceived += ProcessData;
    }

    public void CalibratePlayer() => calibrateNextFrame = true;

    // Αυτή η συνάρτηση καλείται από το Background Thread του Bluetooth
    void ProcessData(string rawData)
    {
        // Κλειδώνουμε το buffer ώστε να είναι ασφαλές το γράψιμο
        lock (dataLock)
        {
            //UnityEngine.Debug.Log($"Received data: {rawData}");
            threadSafeBuffer += rawData;
        }
    }

    // Η Update τρέχει αυστηρά στο Main Thread του Unity
    void Update()
    {
        // 1. Μεταφορά των δεδομένων από το Background Thread στο Main Thread
        lock (dataLock)
        {
            if (!string.IsNullOrEmpty(threadSafeBuffer))
            {
                buffer += threadSafeBuffer;
                threadSafeBuffer = "";
            }
        }

        // 2. Ανάγνωση των πακέτων
        int newlineIdx = buffer.IndexOf('\n');
        while (newlineIdx >= 0)
        {
            string line = buffer.Substring(0, newlineIdx).Trim();
            buffer = buffer.Substring(newlineIdx + 1);

            if (line.StartsWith("ALL:"))
                ParseLine(line);

            newlineIdx = buffer.IndexOf('\n');
        }

        // 3. Εφαρμογή της κίνησης (Slerp) ομαλά σε κάθε καρέ χρησιμοποιώντας το Time.deltaTime σωστά
        if (Elbow.bone != null)
        {
            Elbow.bone.rotation = Quaternion.Slerp(Elbow.bone.rotation, Elbow.targetRotation, Time.deltaTime * Elbow.SmoothSpeed);
        }

        if (Shoulder.bone != null)
        {
            Shoulder.bone.rotation = Quaternion.Slerp(Shoulder.bone.rotation, Shoulder.targetRotation, Time.deltaTime * Shoulder.SmoothSpeed);
        }
    }

    void ParseLine(string line)
    {
        try
        {
            // Remove "ALL:" prefix and split the sensor groups (expected format: ALL:quat1|quat2|quat3)
            string content = line.Substring(4);
            string[] sensors = content.Split('|');

            for (int i = 0; i < sensors.Length; i++)
            {
                Bone b = null;

                // Identify which sensor index corresponds to which bone
                if (i == 0)
                    b = Elbow;
                else if (i == 1)
                    b = Shoulder;

                if (b == null || b.bone == null)
                    continue;

                string[] q = sensors[i].Split(',');

                if (q.Length != 4)
                    continue;

                // Split individual Quaternion components (x,y,z,w)
                float x = float.Parse(q[0], CultureInfo.InvariantCulture);
                float y = float.Parse(q[1], CultureInfo.InvariantCulture);
                float z = float.Parse(q[2], CultureInfo.InvariantCulture);
                float w = float.Parse(q[3], CultureInfo.InvariantCulture);

                // Reconstruct Quaternion and adjust coordinate system (Sensor space to Unity space)
                Quaternion rawQ = new Quaternion(y, -z, -x, w);
                Vector3 euler = rawQ.eulerAngles;

                if (calibrateNextFrame)
                    b.calibrationOffset = rawQ;

                // Calculate the difference between current sensor rotation and calibrated rotation
                float dx = Mathf.DeltaAngle(b.calibrationOffset.eulerAngles.x, euler.x);
                float dy = Mathf.DeltaAngle(b.calibrationOffset.eulerAngles.y, euler.y);
                float dz = Mathf.DeltaAngle(b.calibrationOffset.eulerAngles.z, euler.z);

                // Remap the sensor axes to the intended Unity axes based on user configuration
                float worldX = GetVal(dx, dy, dz, b.Source_For_Pitch) * b.Pitch_Multiplier;
                float worldY = GetVal(dx, dy, dz, b.Source_For_Yaw) * b.Yaw_Multiplier;
                float worldZ = GetVal(dx, dy, dz, b.Source_For_Roll) * b.Roll_Multiplier;

                //showEulerAngles(i, worldX, worldY, worldZ);

                // ΑΛΛΑΓΗ: Αντί να εφαρμόζουμε κατευθείαν το rotation, το αποθηκεύουμε ως Target
                Quaternion deltaRotation = Quaternion.Euler(worldX, worldY, worldZ);
                b.targetRotation = deltaRotation * b.initialWorldRot;
            }
            if (calibrateNextFrame)
                calibrateNextFrame = false;
        }
        catch { }
    }

    //public void showEulerAngles(int sensorId, float x, float y, float z)
    //{
    //    string label = "NULL";

    //    if (sensorId == 1)
    //        label = "ELBOW";
    //    else if (sensorId == 0)
    //        label = "SHOULDER";

    //    string data = $"{label}\n" +
    //                  $"X: {x:F2}°\n" +
    //                  $"Y: {y:F2}°\n" +
    //                  $"Z: {z:F2}°";

    //    // Τώρα είναι απόλυτα ασφαλές να αλλάξουμε το UI text γιατί τρέχει από την Update!
    //    if (sensorId == 0)
    //    {
    //        elbowAngle.text = data;
    //    }
    //    else if (sensorId == 1)
    //    {
    //        shoulderAngle.text = data;
    //    }
    //}

    // Helper to pick the correct float value based on the Enum selection
    float GetVal(float x, float y, float z, SourceAxis axis)
    {
        if (axis == SourceAxis.X)
            return x;
        if (axis == SourceAxis.Y)
            return y;
        return z;
    }
}