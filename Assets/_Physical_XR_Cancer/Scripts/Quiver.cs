using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiver : MonoBehaviour
{
    public Transform HeadsetCamera;
    public Transform Col1;

    public GameObject LeftArrow;
    public GameObject RightArrow;

    // Start is called before the first frame update
    void Awake()
    {
        Col1.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(HeadsetCamera.position.x, HeadsetCamera.position.y - 0.2f, HeadsetCamera.position.z + 0.1f);
        Col1.transform.position = new Vector3(HeadsetCamera.position.x, HeadsetCamera.position.y + 0.35f, HeadsetCamera.position.z - 0.1f);
    }

}
