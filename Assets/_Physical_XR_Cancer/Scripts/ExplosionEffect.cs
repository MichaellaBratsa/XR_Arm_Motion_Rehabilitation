using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    float collapseForce = 8.0f; // 

    void Start()
    {
        // Call this function when you want to trigger the explosion

        //GetChildren();
        //Collapse();
    }



    public Transform[] children;
    public Transform secondChild;
    public void Collapse()
    {
        GetChildren();
        for (int i = 0; i < secondChild.childCount; i++)
        {
            Rigidbody rb = children[i].GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // 
                Vector3 collapseDirection = new Vector3(0, -1, 1); //
                rb.AddForce(collapseDirection.normalized * collapseForce, ForceMode.Impulse); //  
            }
        }
    }

    void GetChildren()
    {
        // Get the second child (index 1)
        secondChild = gameObject.transform.GetChild(1);

        // Initialize an array to hold the children of the second child
        children = new Transform[secondChild.childCount];

        // Populate the array with the children
        for (int j = 0; j < secondChild.childCount; j++)
        {
            children[j] = secondChild.GetChild(j); // Get each child
        }
    }

}

