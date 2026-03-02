using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingArrow : MonoBehaviour
{
    private float speed = 10f;
    public Transform target;
    private Vector3 offset = new Vector3(0, 0, 0.66f);

    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.Find("Archery Target").transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position /*+offset*/, speed * Time.deltaTime);
    }
}
