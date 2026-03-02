using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openRotationY = 120f;       // Y rotation for the open position
    public float closedRotationY = 0f;       // Y rotation for the closed position
    public float rotationSpeed = 2f;         // Speed of the door rotation

    private bool isOpening = false;          // Flag to track if door is opening
    private bool isClosing = false;          // Flag to track if door is closing
    private Quaternion openRotation;         // Target rotation for open position
    private Quaternion closedRotation;       // Target rotation for closed position

    private bool isOpen = false;

    void Start()
    {
        // Set the target rotations for open and closed positions
        openRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, openRotationY, transform.rotation.eulerAngles.z);
        closedRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, closedRotationY, transform.rotation.eulerAngles.z);
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOpen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }           
        }*/

        // Smoothly rotate the door towards the target rotation if opening
        if (isOpening)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * rotationSpeed);
            if (Quaternion.Angle(transform.rotation, openRotation) < 0.1f)
            {
                transform.rotation = openRotation;
                isOpening = false;

                isOpen = true; // Now Door is Open.
            }
        }

        // Smoothly rotate the door towards the target rotation if closing
        if (isClosing)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * rotationSpeed);
            if (Quaternion.Angle(transform.rotation, closedRotation) < 0.1f)
            {
                transform.rotation = closedRotation;
                isClosing = false;

                isOpen = false; // Now Door is Close.
            }
        }
    }

    // Method to start opening the door
    public void OpenDoor()
    {
        isOpening = true;
        isClosing = false;
    }

    // Method to start closing the door
    public void CloseDoor()
    {
        isOpening = false;
        isClosing = true;
    }
}
