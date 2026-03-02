using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbbellManager : MonoBehaviour
{
    public Transform HeadsetCamera;
    public Transform Col1;
    public Transform Col2;
    public Transform Col3;

    public GameObject LeftDumbbell;
    public GameObject RightDumbbell;

    public bool isDumbbellAvailable = true;

    // Start is called before the first frame update
    void Awake()
    {
        LeftDumbbell.SetActive(false);
        RightDumbbell.SetActive(true);

        Col1.gameObject.GetComponent<MeshRenderer>().enabled = false;
        Col2.gameObject.GetComponent<MeshRenderer>().enabled = false;
        Col3.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Col1.position = new Vector3(HeadsetCamera.position.x, HeadsetCamera.position.y - 0.8f, HeadsetCamera.position.z -0.05f);
        Col2.position = new Vector3(HeadsetCamera.position.x, HeadsetCamera.position.y - 0.45f, HeadsetCamera.position.z - 0.35f); //Maybe 0.3f for shorter people
        Col3.position = new Vector3(HeadsetCamera.position.x, HeadsetCamera.position.y - 0.1f, HeadsetCamera.position.z -0.05f);
    }

    public void WaitTimeBeforeCanChangeHandAgain()
    {
        StartCoroutine(waitTimeBeforeCanChangeHandAgain());
    }

    IEnumerator waitTimeBeforeCanChangeHandAgain()
    {
        yield return new WaitForSeconds(0.2f);

        isDumbbellAvailable = true;
    }
}
