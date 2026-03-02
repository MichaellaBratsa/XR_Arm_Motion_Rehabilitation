using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoiceManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip [] EL_Clip;
    public AudioClip [] EN_Clip;

    public int currentClip;
    public GameObject exerciseMenu;

    // Start is called before the first frame update
    void Start()
    {
        //print(EL_Clip[0].length);
        audioSource = GetComponent<AudioSource>();

        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (!MenuManager.IS_MUTE)
        {
            switch (currentScene)
            {
                case 0: // 0. Menu Scene (Neck Exercises)
                    PlayVoice(currentScene);
                    break;
                case 4: // 4. Menu Shoulder (Shoulder Exercises)
                    currentClip = 5; // Play Pain clip
                    PlayVoice(currentClip);
                    break;
                case 8: // 8. Menu Arms (Arms Exercises)
                    currentClip = 5; // Play Pain clip
                    PlayVoice(currentClip);
                    break;
                case 11: // 11. Menu Elbow (Arms Exercises)
                    currentClip = 5; // Play Pain clip
                    PlayVoice(currentClip);
                    break;
                default:
                    exerciseMenu.GetComponent<ExerciseMenu>().OnShowExerciseButton();
                    break;
            }
        }       

        //InvokeRepeating("SelectLanguageVoice", 2f, EL_Clip[currentClip].length + 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayVoice()
    {
        currentClip++;
        if (Language.isEnglish)
        {
            audioSource.clip = EN_Clip[currentClip];
        }
        else
        {
            audioSource.clip = EL_Clip[currentClip];
        }

        audioSource.Play();
    }

    public void PlayVoice(int clipNo)
    {
        currentClip = clipNo;
        if (Language.isEnglish)
        {
            audioSource.clip = EN_Clip[currentClip];
        }
        else
        {
            audioSource.clip = EL_Clip[currentClip];
        }

        audioSource.Play();
    }

    public void StopVoice()
    {
        audioSource.Stop();
    }

    public void MuteVoice()
    {
        audioSource.mute = true;
    }

    public void UnmuteVoice()
    {
        audioSource.mute = false;
    }
}
