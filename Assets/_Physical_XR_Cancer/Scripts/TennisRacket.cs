using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisRacket : MonoBehaviour
{
    public TennisCannon tennisCannon;

    public ExerciseMenu exerciseMenu;
    public GameObject CompletedImageObj;

    bool isHandStretched = false;


    // Start is called before the first frame update
    void Start()
    {
        tennisCannon.LeftRacket.SetActive(false);
        tennisCannon.RightRacket.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Handle racket switching logic
        if (other.gameObject.name == "Left Hand Collider" && gameObject.name == "Right Tennis Racket")
        {
            tennisCannon.Col1.transform.position = new Vector3(1, 1.5f, 3); //Collider is to the LEFT
            tennisCannon.LeftRacket.SetActive(true);
            tennisCannon.RightRacket.SetActive(false);
            print("LEFT Racket Enabled");
        }
        else if (other.gameObject.name == "Right Hand Collider" && gameObject.name == "Left Tennis Racket")
        {
            tennisCannon.Col1.transform.position = new Vector3(-1, 1.5f, 3); //Collider is to the RIGHT
            tennisCannon.LeftRacket.SetActive(false);
            tennisCannon.RightRacket.SetActive(true);
            print("RIGHT Racket Enabled");
        }
        else if (other.gameObject.name == "Col 1")
        {
            isHandStretched = true;
        }
        else if (other.gameObject.name == "Tennis Ball" && isHandStretched)
        {
            // Update ball state
            other.gameObject.name = "hit ball";
            Rigidbody ballRigidbody = other.gameObject.GetComponent<Rigidbody>();
            ballRigidbody.useGravity = true;
            other.gameObject.GetComponent<Rotator>().enabled = false;
            other.gameObject.GetComponent<SphereCollider>().isTrigger = false;
            isHandStretched = false;

            //////////////////////////////////////////////////////////////////////////////////////////////////////

            // Calculate the force direction based on the collision point
            ContactPoint[] contactPoints = new ContactPoint[1];
            Vector3 forceDirection = transform.forward; // Default to racket's forward direction
            if (other.TryGetComponent<Collider>(out Collider ballCollider))
            {
                if (Physics.ComputePenetration(
                    GetComponent<Collider>(),
                    transform.position,
                    transform.rotation,
                    ballCollider,
                    other.transform.position,
                    other.transform.rotation,
                    out Vector3 collisionNormal,
                    out float penetrationDepth))
                {
                    forceDirection = collisionNormal; // Use the collision normal for force direction
                }
            }

            // Add the racket's velocity to the force
            Vector3 racketVelocity = GetComponent<Rigidbody>().linearVelocity; // Ensure the racket has a Rigidbody component
            float forceMagnitude = 10f; // Adjust the force magnitude for realistic hits
            Vector3 totalForce = (forceDirection.normalized * forceMagnitude) + racketVelocity;

            // Apply the calculated force to the ball
            ballRigidbody.AddForce(-totalForce, ForceMode.Impulse);

            //////////////////////////////////////////////////////////////////////////////////////////////////////

            // Destroy the ball after 4 seconds
            Destroy(other.gameObject, 4f);

            // Handle repetition logic
            exerciseMenu.IncreaseRepetition();
            if (exerciseMenu.repetitions == MenuManager.TOTAL_REPETITIONS)
            {
                // Exercise completed
                exerciseMenu.CloseExerciseMenuPanelInstantly();
                CompletedImageObj.SetActive(true);
                GetComponent<BoxCollider>().enabled = false; // turn off collider to remove the ability change hand and stop the call of exerciseMenu.NextScene()
                StartCoroutine(exerciseMenu.NextScene()); // Move to the next scene
            }
            else
            {
                // Shoot the next ball
                StartCoroutine(tennisCannon.shoot());
            }
        }
    }

}