using System.Collections;
using UnityEngine;

public class TennisCannon : MonoBehaviour
{
    public GameObject tennisBallPrefab;  // Prefab for the ball
    public Transform head;       // Target to shoot towards
    float speed = 1300f; // Speed at which the ball moves

    public  GameObject currentBall;    // The currently instantiated ball

    public GameObject LeftRacket;
    public GameObject RightRacket;
    public GameObject Col1;

    private void Start()
    {
        Col1.GetComponent<MeshRenderer>().enabled = true;
        StartCoroutine(shoot());
    }

    void Update()
    {
       
    }


    public IEnumerator shoot()
    {
        yield return new WaitForSeconds(0.5f);

        currentBall = Instantiate(tennisBallPrefab, head.transform.position, Quaternion.identity);
        currentBall.name = "Tennis Ball";
        Rigidbody rb = currentBall.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed * rb.mass);
    }

}
