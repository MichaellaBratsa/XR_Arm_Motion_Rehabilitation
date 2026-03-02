using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour
{
    public Transform basket;         // The basket's position
    public Transform shootPoint;     // Starting position of the ball
    public Transform startTransform;     // Starting position of the ball
    public float curveHeight = 4.0f; // Height of the arc curve towards the basket

    private float shotSpeed = 1.2f;   // Speed of the shot towards the basket
    private float floorSpeed = 2.5f;   // Speed of the shot towards the basket
    private float returnSpeed = 1.5f; // Speed of the return movement to the player

    private Vector3 startPoint;      // Starting position of the shot
    private Vector3 floorPoint;      // Point on the floor to simulate a bounce
    private Vector3 controlPoint;    // Control point for arc movement
    private bool isReadyForShot = true;  // If true, allows shooting
    private bool movingToBasket = false; // Controls the shot-to-basket phase
    private bool movingToFloor = false;  // Controls the descent to floor phase
    private bool returningToPlayer = false; // Controls the return to player phase
    private float t = 0;             // Interpolation factor


    public ExerciseMenu exerciseMenu;
    public GameObject CompletedImageObj;

    void Start()
    {
        GetComponent<Rotator>().enabled = false;       
    }

    void Update()
    {
        // Check if space is pressed and ball is ready to shoot
        /*if (Input.GetKeyDown(KeyCode.Space) && isReadyForShot && exerciseMenu.repetitions< MenuManager.TOTAL_REPETITIONS)
        {
            StartShot();
        }*/

        // Move the ball based on the current phase
        if (movingToBasket)
        {
            ShootTowardsBasket();
        }
        else if (movingToFloor)
        {
            MoveDownToFloor();
        }
        else if (returningToPlayer)
        {
            MoveBackToPlayer();
        }
    }

    // Sets up initial positions and control points
    void InitializePositions()
    {
        startPoint = shootPoint.position;
        floorPoint = new Vector3(basket.position.x, shootPoint.position.y, basket.position.z);
        controlPoint = (startPoint + basket.position) / 2 + Vector3.up * curveHeight;
    }

    // Starts the shot towards the basket
    void StartShot()
    {
        InitializePositions();

        GetComponent<Rotator>().enabled = true;
        isReadyForShot = false;  // Disable shooting until ready again
        movingToBasket = true;   // Start the shot
        t = 0;                   // Reset interpolation
    }

    void ShootTowardsBasket()
    {
        // Increment interpolation factor over time
        t += Time.deltaTime * shotSpeed;

        // Calculate Bezier curve for arc movement
        Vector3 m1 = Vector3.Lerp(startPoint, controlPoint, t);
        Vector3 m2 = Vector3.Lerp(controlPoint, basket.position, t);
        transform.position = Vector3.Lerp(m1, m2, t);

        // Check if we have reached the basket
        if (t >= 1.0f)
        {
            t = 0; // Reset for the next phase
            movingToBasket = false; // Switch phase
            movingToFloor = true;   // Start moving down to the floor
            exerciseMenu.IncreaseRepetition();
        }
    }

    void MoveDownToFloor()
    {
        // Move the ball downwards towards the floor point
        t += Time.deltaTime * floorSpeed;
        transform.position = Vector3.Lerp(basket.position, floorPoint, t);

        // Check if we reached the floor
        if (t >= 1.0f)
        {
            t = 0; // Reset for the next phase
            movingToFloor = false; // Switch phase
            returningToPlayer = true; // Start returning to the player
        }
    }

    void MoveBackToPlayer()
    {
        // Move the ball back to the player
        t += Time.deltaTime * returnSpeed;
        transform.position = Vector3.Lerp(floorPoint, startTransform.position, t);

        // Check if we reached the player
        if (t >= 1.0f)
        {
            // Reset states for reshoot
            t = 0;
            returningToPlayer = false;
            isReadyForShot = true;  // Ball is now ready for another shot
            GetComponent<Rotator>().enabled = false;

            if (exerciseMenu.repetitions == MenuManager.TOTAL_REPETITIONS)
            {
                // Exercise Completed.        
                exerciseMenu.CloseExerciseMenuPanelInstantly();
                CompletedImageObj.SetActive(true);

                // ADD next scene CODE HERE.....
                StartCoroutine(exerciseMenu.NextScene()); //Move to the NEXT SCENE
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");

        if (other.gameObject.name == "Hand Collider")
        {
            this.transform.SetParent(other.transform.parent);
        }

        // Check if space is pressed and ball is ready to shoot
        if (other.gameObject.name == "ArmMaxHeight" && isReadyForShot && exerciseMenu.repetitions < MenuManager.TOTAL_REPETITIONS)
        {
            transform.parent = null;

            StartShot();
        }
    }
}
