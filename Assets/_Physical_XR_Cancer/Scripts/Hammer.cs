using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public NailsManager nailsManager;
    public ExerciseMenu exerciseMenu;
    public GameObject CompletedImageObj;
    public GameObject CanvasCompleted;

    public bool enterUpCol = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Left Hand Collider" && gameObject.transform.parent.name == "Right Hammer")
        {
            nailsManager.LeftHammer.SetActive(true);
            nailsManager.RightHammer.SetActive(false);


            print("LEFT Hammer Enabled");
        }
        else if (other.gameObject.name == "Right Hand Collider" && gameObject.transform.parent.name == "Left Hammer")
        {
            nailsManager.LeftHammer.SetActive(false);
            nailsManager.RightHammer.SetActive(true);

            print("RIGHT Hammer Enabled");
        }
        else if (other.gameObject.name == "Col 1")
        {
            enterUpCol = true;

            print("ENTER UpCol");
        }
        else if (other.gameObject.name == "Nail")
        {
            if (enterUpCol)
            {
                nailsManager.MoveNailDown(other.gameObject);
                enterUpCol = false;

                exerciseMenu.IncreaseRepetition();
                if (exerciseMenu.repetitions == MenuManager.TOTAL_REPETITIONS)
                {
                    // Exercise Completed.        
                    try
                    {
                        Debug.Log("PRE-SAVE");
                        GameObject.Find("SubjectDataSinglenton").GetComponent<SubjectDataSinglenton>().saveInLogFile();
                        Debug.Log("SAVE");
                    }
                    catch
                    {
                        Debug.LogWarning("SubjectDataSinglenton GameObject NOT FOUND!");
                    }
                    
                    exerciseMenu.CloseExerciseMenuPanelInstantly();
                    CompletedImageObj.SetActive(true);

                    // ADD next scene CODE HERE.....
                    GetComponent<BoxCollider>().enabled = false; // turn off collider to remove the ability change hand and stop the call of exerciseMenu.NextScene()
                    StartCoroutine(openMenu());
                    //StartCoroutine(exerciseMenu.NextScene()); //Move to the NEXT SCENE
                }
                else
                {
                    
                    nailsManager.RotateToNextObject();
                }
            }
            print("Nail HIT");
        }
    }

    IEnumerator openMenu()
    {
        yield return new WaitForSeconds(0.1f);
        CanvasCompleted.SetActive(true);
    }
}
