using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
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
        if (this.gameObject.name == "Tennis Ball" && other.gameObject.name == "STOP")
        {
            gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }
}
