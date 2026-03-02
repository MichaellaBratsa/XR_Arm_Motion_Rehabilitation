using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleBasketManager : MonoBehaviour
{

    public GameObject applePrefab;
    public Transform[] spawnPoints;

    public ExerciseMenu exerciseMenu;
    public GameObject CompletedImageObj;

    // Start is called before the first frame update
    void Awake()
    {
        CompletedImageObj.SetActive(false);
        spawnPoints[0].parent.gameObject.SetActive(false); // Disable All spawnPoints from Parent.

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject apple = Instantiate(applePrefab, spawnPoints[i].position, Quaternion.identity);
            apple.name = i.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destroyable")
        {
            if (exerciseMenu.repetitions + spawnPoints.Length < MenuManager.TOTAL_REPETITIONS)
            {
                int i = int.Parse(other.gameObject.name); // int i = the apple Parent.
                GameObject apple = Instantiate(applePrefab, spawnPoints[i].position, Quaternion.identity);
                apple.name = i.ToString();
            }

            Destroy(other.gameObject);
            exerciseMenu.IncreaseRepetition();

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
}
