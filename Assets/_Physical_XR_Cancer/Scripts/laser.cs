using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    public const float TIME_TO_HIT = 1.5f; //time to click
    public const float TIME_TO_CATCH_APPLE = 0.4f; //time to click

    public static int HIT_STATE = 3;

    float hitTimeCounter = 0; 
    bool isPlayed = false;  //isPlayed touch audio
    bool isButtonPressed = false; //if hit other item

    //public GameObject effect;
    public GameObject lastGameObjectButtonCalled;

    public VoiceManager voiceManager;

    public enum hitState // enumn for hit state
    {
        isHitTarget = 0,
        isHitDistractor = 1,
        isHitOther = 2,
        isNotHit = 3,
    }

    public hitState hState; // hitState handler

    // Start is called before the first frame update
    void Start()
    {
        setAllBarsZero("Apple");
        setAllBarsZero("Target"); //make all progressBar zero
        setAllBarsZero("Button"); //make all progressBar zero
    }

    // Update is called once per frame
    void Update()
    {
        HIT_STATE = (int)hState;
        //print("HIT_STATE " + HIT_STATE);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity)) //if hit something
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);

            try
            {
                //Debug.Log("Hover on VR " + hit.collider.gameObject.transform.parent.name); // print Name
            }
            catch
            {

            }

            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, hit.distance * 5f); //scale cylinder Zscale
                                                                                                                   //Debug.Log("hit.distance: " + hit.distance);

            if (hit.collider.gameObject.tag == "Apple") //if tag of hit object is Target
            {
                GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.green); //Color to green

                //start increase ProgressBarCircle
                hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                hit.collider.gameObject.transform.GetChild(0).GetComponentInChildren<ProgressBarCircle>().barValue = hitTimeCounter * 100 / TIME_TO_CATCH_APPLE;

                hitTimeCounter += Time.deltaTime; //increase time hitting
                if (hitTimeCounter > TIME_TO_CATCH_APPLE) //if hitTimeCounter is max
                {
                    hState = hitState.isHitTarget; //hitState.isHitTarget
                    hit.collider.gameObject.transform.parent.parent.SetParent(transform.GetChild(0).GetChild(0));
                    hit.collider.transform.gameObject.SetActive(false);
                    hit.collider.transform.parent.parent.gameObject.GetComponent<SphereCollider>().enabled = true;
                    //hitTimeCounter = 0; //reset counter
                    print("Hit Apple!");
                }
            }
            else if (hit.collider.gameObject.tag == "Target") //if tag of hit object is Target
            {
                GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.green); //Color to green

                /*if (!isPlayed) //if not played touch audio
                {
                    GameObject e = Instantiate(effect, hit.collider.gameObject.transform); // Instansieate effect
                    Destroy(e, 0.15f);

                    GetComponent<AudioSource>().Play(); // play touch Audio

                    isPlayed = true; //isPlayed = true to play only once
                }*/

                //start increase ProgressBarCircle
                hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                hit.collider.gameObject.transform.GetChild(0).GetComponent<ProgressBarCircle>().barValue = hitTimeCounter * 100 / TIME_TO_HIT;


                hitTimeCounter += Time.deltaTime; //increase time hitting
                if (hitTimeCounter > TIME_TO_HIT) //if hitTimeCounter is max
                {
                    hState = hitState.isHitTarget; //hitState.isHitTarget
                    //visSearComponent.reactionTime = visSearComponent.TIME_TO_PLAYER_TRYING - visSearComponent.tryingUserCounter - TIME_TO_HIT;// + 0.05f; //set reactionTime
                    //visSearComponent.tryingUserCounter = visualSearchManager.END_TRYING_TIME; //set almost zero value to end

                    hitTimeCounter = 0; //reset counter
                    //print("Hit Target!");
                }
            }
            else if (hit.collider.gameObject.tag == "Button") //if tag of hit object is Distractor
            {
                GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.yellow); //Color it yellow                

                /*if (!isPlayed) //if not played touch audio
                {
                    GameObject e = Instantiate(effect, hit.collider.gameObject.transform);
                    Destroy(e, 0.15f);

                    GetComponent<AudioSource>().Play(); // play touch Audio

                    isPlayed = true; //isPlayed = true to play only once
                }*/

                //start increase ProgressBarCircle
                hit.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                try
                {
                    hit.collider.gameObject.transform.GetChild(0).GetComponent<ProgressBarCircle>().barValue = hitTimeCounter * 100 / TIME_TO_HIT;
                }
                catch
                {

                }

                hitTimeCounter += Time.deltaTime; //increase time hitting
                if (hitTimeCounter > TIME_TO_HIT) //if hitTimeCounter is max
                {
                    hState = hitState.isHitDistractor; //hitState.isHitDistractor

                    if (!isButtonPressed) //if not Button Pressed
                    {
                        lastGameObjectButtonCalled = hit.collider.transform.parent.gameObject;
                        if (voiceManager != null)
                        {
                            voiceManager.StopVoice();
                        }
                        hit.collider.gameObject.GetComponentInParent<Button>().onClick.Invoke(); // Call Button Method
                        isButtonPressed = true; // make it true to count only once
                    }
                    
                    //visSearComponent.tryingUserCounter = visualSearchManager.END_TRYING_TIME; //set almost zero value

                    hitTimeCounter = TIME_TO_HIT; //set counter to max to stop fill progressBar
                    //print("Hit No Distractor :(");
                }
                //print("Time " + hitTimeCounter * 100 / TIME_TO_HIT);
            }
            else //if you hit any other item
            {
                GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.yellow); //Color it yellow

                hState = hitState.isHitOther; //hitState.isHitOther
                isPlayed = false;

                hitTimeCounter = 0;
                setAllBarsZero("Apple");
                setAllBarsZero("Target"); //make all progressBar zero
                setAllBarsZero("Button"); //make all progressBar zero
                isButtonPressed = false;
            }
        }
        else // if not hit something
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 10, Color.red);
            Debug.Log("Not Hit");

            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 40);
            GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.red);

            hState = hitState.isNotHit;
            isPlayed = false;

            hitTimeCounter = 0;
            setAllBarsZero("Apple");
            setAllBarsZero("Target");
            setAllBarsZero("Button"); //make all progressBar zero
            isButtonPressed = false;
        }
    }


    public GameObject[] allBarsWillBeZero;
    void setAllBarsZero(string tagName)
    {
        allBarsWillBeZero = GameObject.FindGameObjectsWithTag(tagName);

        for (int i = 0; i < allBarsWillBeZero.Length; i++)
        {
            try
            {
                GameObject child = allBarsWillBeZero[i].transform.GetChild(0).gameObject;
                child.SetActive(false);
                //child.transform.parent.parent.gameObject.GetComponent<SphereCollider>().enabled = true;
                child.transform.GetChild(0).GetComponentInChildren<ProgressBarCircle>().barValue = hitTimeCounter;
                print("BarValue: " + child.transform.GetChild(0).GetComponentInChildren<ProgressBarCircle>().barValue);
            }
            catch
            {

            }
        }
    }
}
