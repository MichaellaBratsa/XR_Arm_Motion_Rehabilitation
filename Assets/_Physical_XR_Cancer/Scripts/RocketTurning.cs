using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketTurning : MonoBehaviour
{
    readonly float MAX_MOMENTUM = 1.5f;
    float momentum = 1.1f;

    public GameObject mainCam;

    public bool goingLeft;

    public bool left = false;
    public bool right = false;

    public ExerciseMenu exerciseMenu;
    public GameObject CompletedImageObj;


    public GameObject [] BricksPrefabs;
    public GameObject [] BricksObjs;

    // Start is called before the first frame update
    void Awake()
    {
        CompletedImageObj.SetActive(false);
        SpawnBricks();
    }

    void SpawnBricks()
    {
        BricksObjs = new GameObject[BricksPrefabs.Length];

        for (int i = 0; i < BricksPrefabs.Length; i++)
        {
            BricksObjs[i] = Instantiate(BricksPrefabs[i]);
        }
    }

    void DestroyBricks()
    {
        for (int i = 0; i < BricksObjs.Length; i++)
        {
            Destroy(BricksObjs[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Rocket Rotation is the same with Headset
        GameObject RocketParent = transform.parent.gameObject;
        RocketParent.transform.eulerAngles = new Vector3(RocketParent.transform.eulerAngles.x, mainCam.transform.eulerAngles.y +180, RocketParent.transform.eulerAngles.z); //change Rocket X-Axis Rotation  
        // Rocket Rotation is the same with Headset

    }

    public void OnTriggerEnter(Collider other)
    {
        print("OnTriggerEnter");

        if (other.gameObject.name == "Collider (Left)")
        {
            other.gameObject.transform.parent.GetComponent<ExplosionEffect>().Collapse();
            left = true;
            Destroy(other.gameObject);
            //transform.Rotate(0, 0, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (other.gameObject.name == "Collider (Right)")
        {
            other.gameObject.transform.parent.GetComponent<ExplosionEffect>().Collapse();
            right = true;
            Destroy(other.gameObject);
            //transform.Rotate(0, 180, 0);
            transform.localRotation = Quaternion.Euler(0, 180, 90);
        }
 
        if (other.gameObject.name == "RESPAWN COLLIDER" && left == true && right == true)
        {
            print("RESPAWN COLLIDER");
            exerciseMenu.IncreaseRepetition();

            if (exerciseMenu.repetitions == MenuManager.TOTAL_REPETITIONS)
            {
                // Exercise Completed.        
                exerciseMenu.CloseExerciseMenuPanelInstantly();
                CompletedImageObj.SetActive(true);

                // ADD next scene CODE HERE.....
                StartCoroutine(exerciseMenu.NextScene()); //Move to the NEXT SCENE
            }
            else
            {
                DestroyBricks();
                SpawnBricks();
                left = right = false;
            }
        }
    }
}

