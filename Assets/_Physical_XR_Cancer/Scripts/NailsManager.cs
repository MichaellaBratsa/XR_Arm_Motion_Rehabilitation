using UnityEngine;

public class NailsManager : MonoBehaviour
{
    public GameObject LeftHammer;
    public GameObject RightHammer;

    public Transform Col1;
    public Transform HeadsetCamera;
    public GameObject NailPrefab; // The prefab to instantiate

    private int numberOfObjects = 30; // Total number of objects
    float radius = 1.4f; // Radius of the circle
    private int currentIndex = 0; // Tracks the current object in view
    private float rotationSpeed = 60f; // Speed of rotation in degrees per second
    private bool isRotating = false; // Determines if the table is currently rotating
    private float targetRotation; // The target Y rotation of the table

    void Awake()
    {
        LeftHammer.SetActive(false);
        RightHammer.SetActive(true);

        Col1.gameObject.GetComponent<MeshRenderer>().enabled = false;

        numberOfObjects = MenuManager.TOTAL_REPETITIONS; // Set to a fixed number for testing purposes
        //numberOfObjects = 30; // You can change this to test with other numbers
        InstantiateNailsInCircle();
    }

    void Update()
    {
        // This is for TESTING
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            RotateToNextObject();
        }*/

        Col1.position = new Vector3(HeadsetCamera.position.x, HeadsetCamera.position.y + 0.1f, HeadsetCamera.position.z + 0.05f);

        if (isRotating)
        {
            // Smoothly rotate the table towards the target rotation
            float currentRotation = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, currentRotation, 0);

            // Check if the rotation has reached the target
            if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetRotation)) < 0.1f)
            {
                isRotating = false;
                transform.eulerAngles = new Vector3(0, targetRotation, 0); // Snap to the exact target
            }
        }
    }

    void InstantiateNailsInCircle()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Calculate the angle for each object
            float angle = i * Mathf.PI * 2 / numberOfObjects;

            // Determine the position on the circle relative to the center transform
            Vector3 position = transform.position + new Vector3(Mathf.Sin(angle) * radius, 1.31f, Mathf.Cos(angle) * radius); // Assuming objects are on the same horizontal plane

            // Instantiate the object at the calculated position
            GameObject nail = Instantiate(NailPrefab, position, Quaternion.Euler(90, 0, 0));
            nail.name = "Nail";
            nail.transform.parent = this.transform;
        }
    }

    public void RotateToNextObject()
    {
        if (isRotating) return; // Avoid starting a new rotation if one is already in progress

        // Update the current index to the next object
        currentIndex = (currentIndex + 1) % numberOfObjects;

        // Calculate the target rotation for the table
        float anglePerObject = 360f / numberOfObjects;
        targetRotation = currentIndex * anglePerObject;

        // Start rotating the table
        isRotating = true;
    }

    public void MoveNailDown(GameObject Nail)
    {
        // Move the nail down by 0.12 units
        Nail.transform.position += new Vector3(0, -0.12f, 0); // Adjust y position to move down
        Destroy(Nail.GetComponent<BoxCollider>());
    }
}