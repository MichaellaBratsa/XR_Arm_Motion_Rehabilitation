using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumbbell : MonoBehaviour
{
    public enum DumbbellState
    {
        Idle,
        AtBottom,
        Bot_Mid,  
        Mid_TOP,   
        Top_Mid,   
        Mid_Bot
    }

    public DumbbellState currentState;

    public DumbbellManager dumbbellManager;
    public TreesScalerManager treesScalerManager;
    public ExerciseMenu exerciseMenu;
    public GameObject CompletedImageObj;

    // Start is called before the first frame update
    void Awake()
    {
        currentState = DumbbellState.Idle; // Initial state
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (dumbbellManager.isDumbbellAvailable)
        {
            if (other.gameObject.name == "Left Hand Collider" && gameObject.name == "Right Dumbbell")
            {
                dumbbellManager.LeftDumbbell.SetActive(true);
                dumbbellManager.RightDumbbell.SetActive(false);

                dumbbellManager.isDumbbellAvailable = false;
                dumbbellManager.WaitTimeBeforeCanChangeHandAgain(); //Wait Time Before Can Change Hand Again

                print("LEFT Dumbbell Enabled");
            }
            else if (other.gameObject.name == "Right Hand Collider" && gameObject.name == "Left Dumbbell")
            {
                dumbbellManager.LeftDumbbell.SetActive(false);
                dumbbellManager.RightDumbbell.SetActive(true);

                dumbbellManager.isDumbbellAvailable = false;
                dumbbellManager.WaitTimeBeforeCanChangeHandAgain(); //Wait Time Before Can Change Hand Again

                print("RIGHT Dumbbell Enabled");
            }
        }

        if (currentState == DumbbellState.Idle) // this force  user start from the bottom
        {
            if (other.gameObject.name == "Col 1")
            {
                currentState = DumbbellState.AtBottom;
            }
            else
            {
                currentState = DumbbellState.Idle;
            }
        }
        else if (currentState == DumbbellState.AtBottom)
        {
            if (other.gameObject.name == "Col 2")
            {
                currentState = DumbbellState.Bot_Mid;
            }
        }
        else if (currentState == DumbbellState.Bot_Mid)
        {
            if (other.gameObject.name == "Col 3")
            {
                currentState = DumbbellState.Mid_TOP;
            }
        }
        else if (currentState == DumbbellState.Mid_TOP)
        {
            if (other.gameObject.name == "Col 2")
            {
                currentState = DumbbellState.Top_Mid;
            }
        }
        else if (currentState == DumbbellState.Top_Mid)
        {
            currentState = DumbbellState.AtBottom; //Completed

            exerciseMenu.IncreaseRepetition();
            treesScalerManager.ScaleRepetitionTree();

            if (exerciseMenu.repetitions == MenuManager.TOTAL_REPETITIONS)
            {
                // Exercise Completed.        
                exerciseMenu.CloseExerciseMenuPanelInstantly();
                CompletedImageObj.SetActive(true);

                // ADD next scene CODE HERE.....
                GetComponent<BoxCollider>().enabled = false; // turn off collider to remove the ability change hand and stop the call of exerciseMenu.NextScene()
                StartCoroutine(exerciseMenu.NextScene()); //Move to the NEXT SCENE
            }
        }
    }

}
