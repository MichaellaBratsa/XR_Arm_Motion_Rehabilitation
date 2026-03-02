using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScaler : MonoBehaviour
{
    private float MAX_SCALE = 1f;
    private float SCALING_SPEED = 0.5f;

    private float scaleUpdater;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scaleUpdater < MAX_SCALE) // if flower scaleY is not MAX then
        {
            scaleUpdater += Time.deltaTime * SCALING_SPEED;
            //transform.localScale = new Vector3(transform.localScale.x, scaleUpdater, transform.localScale.z);
            transform.localScale = new Vector3(scaleUpdater, scaleUpdater, scaleUpdater);
        }
        else
        {
            transform.localScale = new Vector3(MAX_SCALE, MAX_SCALE, MAX_SCALE);
        }
    }
}
