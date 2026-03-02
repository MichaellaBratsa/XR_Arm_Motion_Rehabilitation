using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesScalerManager : MonoBehaviour
{
    public GameObject Trees;
    public GameObject [] Tree;

    public ExerciseMenu exerciseMenu;

    // Start is called before the first frame update
    void Awake()
    {
        // Initialize the array to match the number of children
        int childCount = Trees.transform.childCount;
        Tree = new GameObject[childCount];

        // Populate the array with child GameObjects
        for (int i = 0; i < childCount; i++)
        {
            Tree[i] = Trees.transform.GetChild(i).gameObject;
            Tree[i].transform.localScale = new Vector3(0, 0, 0);
            Tree[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScaleRepetitionTree()
    {
        Tree[exerciseMenu.repetitions -1].SetActive(true);
    }
}
