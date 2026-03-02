using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyNet : MonoBehaviour
{
    public ButterflySpawner butterflySpawner;
    public DoorController doorController;


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
        if (other.gameObject.name == "Left Hand Collider" && gameObject.name == "Right ButterflyNet")
        {
            butterflySpawner.LeftButterflyNet.SetActive(true);
            butterflySpawner.RightButterflyNet.SetActive(false);

            butterflySpawner.SetLeftHandButterfly();

            print("LEFT ButterflyNet Enabled");
        }
        else if (other.gameObject.name == "Right Hand Collider" && gameObject.name == "Left ButterflyNet")
        {
            butterflySpawner.LeftButterflyNet.SetActive(false);
            butterflySpawner.RightButterflyNet.SetActive(true);

            butterflySpawner.SetRightHandButterfly();
            print("RIGHT ButterflyNet Enabled");
        }
        else if (other.gameObject.name == "Butterfly")
        {
            if (!butterflySpawner.CurrentButterfly.GetComponent<Butterfly>().isDoneFromTarget) // if  NOT isDonefromTarget
            {
                butterflySpawner.CurrentButterfly.GetComponent<Butterfly>().isMovingToTarget = true;

                if (gameObject.name == "Right ButterflyNet")
                {
                    transform.localRotation = Quaternion.Euler(180, -90, -25);
                }
                else if (gameObject.name == "Left ButterflyNet")
                {
                    transform.localRotation = Quaternion.Euler(65, 0, 0);
                }                    
            }
            else if (butterflySpawner.CurrentButterfly.GetComponent<Butterfly>().isDoneFromTarget && !butterflySpawner.CurrentButterfly.GetComponent<Butterfly>().isDoneFromCage) // if  (isDonefromTarget && NOT isDoneFromCage)
            {
                butterflySpawner.CurrentButterfly.GetComponent<Butterfly>().isMovingToCage = true;

                if (gameObject.name == "Right ButterflyNet")
                {
                    transform.localRotation = Quaternion.Euler(0, -90, 25);
                }
                else if (gameObject.name == "Left ButterflyNet")
                {
                    transform.localRotation = Quaternion.Euler(-65, -180, 0);
                }
                

                doorController.OpenDoor();
            }
            else if (butterflySpawner.CurrentButterfly.GetComponent<Butterfly>().isDoneFromTarget && butterflySpawner.CurrentButterfly.GetComponent<Butterfly>().isDoneFromCage) // if  (isDonefromTarget && NOT isDoneFromCage)
            {
                
            }

        }

    }
}
