using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottleRotationTracking : MonoBehaviour
{
    float MAX_TILTING = 0.25f;

    public GameObject Bottle;
    float momentum = 0f;

    public bool left = false;
    public bool right = false;

    public ExerciseMenu exerciseMenu;
    public GameObject CompletedImageObj;

    public GameObject PouringParticlePrefab;

    // Start is called before the first frame update
    void Awake()
    {
        CompletedImageObj.SetActive(false);
        Bottle.transform.GetChild(0).gameObject.SetActive(false); // stop pouring
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate Bottle
        if (transform.rotation.z < 0)
        {
            Bottle.transform.eulerAngles = new Vector3(Bottle.transform.eulerAngles.x, Bottle.transform.eulerAngles.y, transform.eulerAngles.z + momentum); //change Bottle Z-Axis Rotation
        }
        else
        {
            Bottle.transform.eulerAngles = new Vector3(Bottle.transform.eulerAngles.x, Bottle.transform.eulerAngles.y, transform.eulerAngles.z - momentum); //change Bottle Z-Axis Rotation
        }
        // Rotate Bottle


        if (transform.rotation.z > MAX_TILTING)
        {
            left = true;
            Bottle.transform.GetChild(0).gameObject.SetActive(true); // start pouring
        }
        else if (transform.rotation.z < -MAX_TILTING)
        {
            right = true;
            Bottle.transform.GetChild(0).gameObject.SetActive(true); // start pouring
        }
        else if (exerciseMenu.repetitions < MenuManager.TOTAL_REPETITIONS && left == true && right == true && (transform.rotation.z > -0.1 && transform.rotation.z < 0.1))
        {
            left = false;
            right = false;
            
            exerciseMenu.IncreaseRepetition();
        }
        else if (exerciseMenu.repetitions == MenuManager.TOTAL_REPETITIONS)
        {
            // Exercise Completed.        
            exerciseMenu.CloseExerciseMenuPanelInstantly();
            CompletedImageObj.SetActive(true);

            // ADD next scene CODE HERE.....
            StartCoroutine(exerciseMenu.NextScene()); //Move to the NEXT SCENE
        }
        else if (transform.rotation.z > -MAX_TILTING && transform.rotation.z < MAX_TILTING)
        {
            Bottle.transform.GetChild(0).gameObject.SetActive(false); // stop pouring
        }
        //print("Headset: " + transform.eulerAngles.z + "  Bottle: " + Bottle.transform.eulerAngles.z);
        //print("Headset Rot: " + transform.rotation.z);
    }

}
