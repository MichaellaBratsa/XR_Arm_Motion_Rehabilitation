using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    readonly float BUTTERFLY_POSITION_X = 1.3f;

    public GameObject ButterflyPrefab;
    public GameObject Target;
    public GameObject CurrentButterfly;

    public GameObject LeftButterflyNet;
    public GameObject RightButterflyNet;

    public GameObject CompletedImageObj;

    // Start is called before the first frame update
    void Awake()
    {
        Target.GetComponent<MeshRenderer>().enabled = false;
        LeftButterflyNet.SetActive(false);
        RightButterflyNet.SetActive(true);

        SpawnButterfly();
        SetRightHandButterfly();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetLeftHandButterfly()
    {
        ButterflyPrefab.transform.position = new Vector3(BUTTERFLY_POSITION_X, ButterflyPrefab.transform.position.y, ButterflyPrefab.transform.position.z);
        CurrentButterfly.transform.position = new Vector3(BUTTERFLY_POSITION_X, ButterflyPrefab.transform.position.y, ButterflyPrefab.transform.position.z);
        Target.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z);
    }
    public void SetRightHandButterfly()
    {
        ButterflyPrefab.transform.position = new Vector3(-BUTTERFLY_POSITION_X, ButterflyPrefab.transform.position.y, ButterflyPrefab.transform.position.z);
        CurrentButterfly.transform.position = new Vector3(-BUTTERFLY_POSITION_X, ButterflyPrefab.transform.position.y, ButterflyPrefab.transform.position.z);
        Target.transform.position = new Vector3(-Target.transform.position.x, Target.transform.position.y, Target.transform.position.z);
    }

    public void SpawnButterfly()
    {
        CurrentButterfly = Instantiate(ButterflyPrefab);
        CurrentButterfly.name = "Butterfly";
    }

}
