using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] fireworkClip;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        int currentClip = Random.Range(0, fireworkClip.Length);
        print("currentClip: " + currentClip);
        audioSource.clip = fireworkClip[currentClip];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
