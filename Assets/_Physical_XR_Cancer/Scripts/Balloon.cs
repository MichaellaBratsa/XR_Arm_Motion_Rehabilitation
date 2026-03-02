using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private float speed = 2.5f;   // Speed of the shot towards the basket

    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Balloon>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpToSky();
    }

    void MoveUpToSky()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

}