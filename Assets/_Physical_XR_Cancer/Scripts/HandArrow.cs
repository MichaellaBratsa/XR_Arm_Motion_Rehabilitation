using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandArrow : MonoBehaviour
{
    private bool LeftEnterQuiver = false;
    private bool LeftEnterBow = false;
    private bool RightEnterQuiver = false;
    private bool RightEnterBow = false;

    public Animator animController;

    public Quiver quiver;
    public GameObject BowArrow;
    public GameObject ArrowPrefab;
    public Transform SpawnPointArrow;
    public GameObject handArrow;
    public ExerciseMenu exerciseMenu;
    public GameObject CompletedImageObj;

    public bool isRightEnterCol1 = false;
    public bool isLeftEnterCol1 = false;

    // Start is called before the first frame update
    void Awake()
    {
        animController.SetBool("Fire", false);
        BowArrow.SetActive(false);

        ResetLeft(); // RESET
        ResetRight(); // RESET
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetLeft()
    {
        LeftEnterQuiver = false;
        LeftEnterBow = false;
        isLeftEnterCol1 = false;

        quiver.LeftArrow.SetActive(false);
    }

    private void ResetRight()
    {
        RightEnterQuiver = false;
        RightEnterBow = false;
        isRightEnterCol1 = false;

        quiver.RightArrow.SetActive(false);
    }

    IEnumerator shootArrow()
    {
        BowArrow.SetActive(true);
        animController.SetBool("Fire", true);
        handArrow.SetActive(false);
        
        yield return new WaitForSeconds(0.5f);
        BowArrow.SetActive(false);
        GameObject arrow = Instantiate(ArrowPrefab, SpawnPointArrow.position, SpawnPointArrow.rotation);
        animController.SetBool("Fire", false);
        // Play Animation.

        Destroy(arrow, 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (exerciseMenu.repetitions == MenuManager.TOTAL_REPETITIONS)
        {
            return;
        }

        if (this.gameObject.name == "Left Hand Collider" && other.gameObject.name == "Col 1")
        {
            isLeftEnterCol1 = true;
            print("LEFT enter Col 1");
        }
        else if (this.gameObject.name == "Right Hand Collider" && other.gameObject.name == "Col 1")
        {
            isRightEnterCol1 = true;
            print("Right enter Col 1");
        }

        if (this.gameObject.name == "Left Hand Collider" && other.gameObject.name == "Quiver Collider" && isLeftEnterCol1)
        {
            handArrow.SetActive(true);

            LeftEnterQuiver = true;
            LeftEnterBow = false;

            ResetRight();

            print("LEFT enter Quiver");
        }
        else if (this.gameObject.name == "Left Hand Collider" && other.gameObject.name == "Crossbow")
        {        
            LeftEnterBow = true;

            if (LeftEnterQuiver)
            {
                StartCoroutine(shootArrow());

                exerciseMenu.IncreaseRepetition();
                if (exerciseMenu.repetitions == MenuManager.TOTAL_REPETITIONS)
                {
                    // Exercise Completed.        
                    exerciseMenu.CloseExerciseMenuPanelInstantly();
                    CompletedImageObj.SetActive(true);

                    // ADD next scene CODE HERE.....
                    StartCoroutine(exerciseMenu.NextScene()); //Move to the NEXT SCENE
                }

                ResetLeft(); // RESET
                ResetRight(); // RESET
            }

            print("LEFT enter Crossbow");
        }

        if (this.gameObject.name == "Right Hand Collider" && other.gameObject.name == "Quiver Collider" && isRightEnterCol1)
        {
            handArrow.SetActive(true);

            RightEnterQuiver = true;
            RightEnterBow = false;

            ResetLeft();

            print("RIGHT enter Quiver");
        }
        else if (this.gameObject.name == "Right Hand Collider" && other.gameObject.name == "Crossbow")
        {
            RightEnterBow = true;

            if (RightEnterQuiver)
            {
                StartCoroutine(shootArrow());

                exerciseMenu.IncreaseRepetition();
                if (exerciseMenu.repetitions == MenuManager.TOTAL_REPETITIONS)
                {
                    // Exercise Completed.        
                    exerciseMenu.CloseExerciseMenuPanelInstantly();
                    CompletedImageObj.SetActive(true);

                    // ADD next scene CODE HERE.....
                    StartCoroutine(exerciseMenu.NextScene()); //Move to the NEXT SCENE
                }


                ResetLeft();  // RESET
                ResetRight(); // RESET
            }

            print("RIGHT enter Crossbow");
        }

    }
}
