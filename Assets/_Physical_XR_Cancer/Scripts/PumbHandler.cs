using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumbHandler : MonoBehaviour
{
    public float MIN_HEIGHT = 1.0f;  // Balloon MIN_HEIGHT = 1.1f; --> Fireworks MIN_HEIGHT = 0.9f;
    public float MAX_HEIGHT = 1.55f;  // Balloon MAX_HEIGHT = 1.9f; --> Fireworks MAX_HEIGHT = 2.2f;
    public float MARGIN = 0.15f;      // Balloon MARGIN = 0.15f;    --> Fireworks MARGIN = 0.3f;   


    public float handlerHeight;
    bool isAttached = false;

    Transform handController;

    public bool up = false;
    public bool down = false;

    public ExerciseMenu exerciseMenu;
    public GameObject CompletedImageObj;

    public Balloon balloon;

    public GameObject FireworksPrefab;
    public bool isFireworkOn = false;


    private void Awake()
    {
        handlerHeight = MAX_HEIGHT - MARGIN;
        transform.position = new Vector3(transform.position.x, handlerHeight, transform.position.z);
    }

    void Update()
    {
        if (isAttached && handlerHeight >= MIN_HEIGHT && handlerHeight <= MAX_HEIGHT)
        {
            handlerHeight = handController.position.y;
            transform.position = new Vector3(transform.position.x, handlerHeight, transform.position.z);
        }
        
        if (handlerHeight < MIN_HEIGHT)
        {
            handlerHeight = MIN_HEIGHT;
            transform.position = new Vector3(transform.position.x, handlerHeight, transform.position.z);
        }
        else if (handlerHeight > MAX_HEIGHT)
        {           
            handlerHeight = MAX_HEIGHT;
            transform.position = new Vector3(transform.position.x, handlerHeight, transform.position.z);
        }



        if (handlerHeight >= MAX_HEIGHT - MARGIN /*&& up == false*/)
        {
            up = true;
        }
        else if (handlerHeight <= MIN_HEIGHT + MARGIN /*&& down == false*/)
        {
            down = true;
        }
        else if (exerciseMenu.repetitions < MenuManager.TOTAL_REPETITIONS && up == true && down == true && (handlerHeight > MIN_HEIGHT + (MAX_HEIGHT - MIN_HEIGHT) / 2 - 0.1 && handlerHeight < MAX_HEIGHT - (MAX_HEIGHT - MIN_HEIGHT) / 2 + 0.1))
        {
            up = false;
            down = false;

            isFireworkOn = false;

            exerciseMenu.IncreaseRepetition();
        }
        else if (exerciseMenu.repetitions == MenuManager.TOTAL_REPETITIONS)
        {
            // Exercise Completed.        
            exerciseMenu.CloseExerciseMenuPanelInstantly();
            CompletedImageObj.SetActive(true);

            if (balloon != null) // if you are on the Balloon Exercise
            {
                balloon.enabled = true;
            } 

            // ADD next scene CODE HERE.....
            StartCoroutine(exerciseMenu.NextScene()); //Move to the NEXT SCENE
        }

        if (FireworksPrefab != null && isFireworkOn == false) // if you are on the Fireworks Exercise
        {
            if (up == true && down == true)
            {
                //Instantiate Fireworks...
                GameObject firework = Instantiate(FireworksPrefab);
                isFireworkOn = true;
                Destroy(firework, 2f);

                print("firework firework firework");
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Left Hand Collider" || other.name == "Right Hand Collider")
        {
            isAttached = true;
            handController = other.transform;
        }
            
        print("OnTriggerEnter " + other.name);
    }

    private void OnTriggerStay(Collider other)
    {
        /*print("OnTriggerStay " + other.name);

        if (other.name == "Right Hand Collider")
        {
            handlerHeight = other.transform.parent.transform.position.y;
        }

        transform.position = new Vector3(transform.position.x, handlerHeight, transform.position.z);*/
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Left Hand Collider" || other.name == "Right Hand Collider")
        {
            isAttached = false;
            handController = null;
        }
        print("OnTriggerExit " + other.name);
    }
}
