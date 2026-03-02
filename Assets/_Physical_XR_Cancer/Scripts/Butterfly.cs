using UnityEngine;

public class Butterfly : MonoBehaviour
{
    public GameObject Door;
    public ExerciseMenu exerciseMenu;

    public Transform target;          // Target GameObject to move towards
    //public BoxCollider cageCollider; // Assign your Box Collider in the Inspector
    public Vector3 cageTarget;


    float moveSpeed = 5f;      // Speed at which the butterfly moves towards the target
    float rotationSpeed = 20f;  // Speed of the rotation to face the target
    public bool isMovingToTarget = false;    // Flag to check if the butterfly should be moving to target
    public bool isMovingToCage = false;    // Flag to check if the butterfly should be moving to cage

    public bool isDoneFromTarget = false;    // Flag to check if the butterfly should be moving to cage
    public bool isDoneFromCage = false;    // Flag to check if the butterfly should be moving to cage

    private void Awake()
    {
        Door = GameObject.Find("Door");
        exerciseMenu = GameObject.Find("ExerciseMenu").GetComponent<ExerciseMenu>();

        target = GameObject.Find("Target").transform;
        //cageCollider = GameObject.Find("Cage").GetComponent<BoxCollider>();
        cageTarget = GetRandomPosition();
    }

    void Update()
    {
        // Check if space key is pressed to start rotating and moving
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            isMovingToTarget = true;
        }*/

        // Rotate and move the butterfly towards the target if isMoving is true
        if (isMovingToTarget)
        {
            RotateTowardsTarget();
        }
        else if (isMovingToCage)
        {
            RotateTowardsTarget();
        }
    }

    void RotateTowardsTarget()
    {
        // Calculate direction to the target
        Vector3 direction = (target.position - transform.position).normalized;

        if (isMovingToTarget)
        {
            direction = -(target.position - transform.position).normalized;
            
        }
        else if (isMovingToCage)
        {
            direction = (target.position - transform.position).normalized;
        }
        else
        {
            direction = (target.position - transform.position).normalized;
        }

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate the butterfly towards the target
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // Check if butterfly is facing the target (close enough in angle)
        if (Quaternion.Angle(transform.rotation, lookRotation) < 1f)
        {
            // Start moving once butterfly is aligned with the target
            //MoveTowardsTarget();

            if (isMovingToTarget)
            {
                MoveTowardsTarget();
            }
            else if (isMovingToCage)
            {
                MoveTowardsCage();
            }
        }
    }

    void MoveTowardsTarget()
    {
        // Move the butterfly towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        // Stop moving if the butterfly is close enough to the target
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            isMovingToTarget = false;
            isDoneFromTarget = true;
        }
    }

    void MoveTowardsCage()
    {
        // Move the butterfly towards the target
        //transform.position = Vector3.MoveTowards(transform.position, cageTarget.position, moveSpeed * 2 * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, cageTarget, moveSpeed * 2 * Time.deltaTime);

        // Stop moving if the butterfly is close enough to the target
        if (Vector3.Distance(transform.position, cageTarget) < 0.01f)
        {
            isMovingToCage = false;
            isDoneFromCage = true;
        }
    }

    // Returns a random position within the Box Collider bounds
    public Vector3 GetRandomPosition()
    {
        /*if (cageCollider == null)
        {
            Debug.LogError("BoxCollider is not assigned!");
            return Vector3.zero;
        }

        // Get the bounds of the Box Collider
        Bounds bounds = cageCollider.bounds;*/

        // Calculate a random position within the bounds
        float x = Random.Range(-0.3f, 0.3f);
        float y = Random.Range(0.5f, 1.9f);
        float z = Random.Range(-4.3f, -5.6f);

        return new Vector3(x, y, z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cage")
        {
            moveSpeed = 0.2f;
            exerciseMenu.IncreaseRepetition();
            Door.GetComponent<DoorController>().CloseDoor();

            if (exerciseMenu.repetitions == MenuManager.TOTAL_REPETITIONS)
            {
                // Exercise Completed.        
                exerciseMenu.CloseExerciseMenuPanelInstantly();
                GameObject.Find("ButterflySpawner").GetComponent<ButterflySpawner>().CompletedImageObj.SetActive(true);

                // ADD next scene CODE HERE.....
                StartCoroutine(exerciseMenu.NextScene()); //Move to the NEXT SCENE
            }
            else
            {
                GameObject.Find("ButterflySpawner").GetComponent<ButterflySpawner>().SpawnButterfly();
            }
          
            print("Butterfly is on Cage");
        }
    }

}