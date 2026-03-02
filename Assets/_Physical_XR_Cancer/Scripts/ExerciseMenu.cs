using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExerciseMenu : MonoBehaviour
{
    public int repetitions = 0;
    public TMP_Text RepetitionsText;
    public TMP_Text scoreText;

    public VoiceManager voiceManager;
    public GameObject ExercisePanel;

    public GameObject ShowExerciseButton;

    // Start is called before the first frame update
    void Awake()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene == 5 || currentScene == 6 || currentScene == 7 || currentScene == 9 || currentScene == 12 || currentScene == 13 || currentScene == 14) 
        {   
            Physics.queriesHitTriggers = false;  // Turn OFF queries hit triggers
        }
        else
        {
            Physics.queriesHitTriggers = true;  // Turn ON queries hit triggers
        }

        if (currentScene == 6)
        {
            //Time
            Time.fixedDeltaTime = 0.01f;
        }
        else
        {
            Time.fixedDeltaTime = 0.02f;
        }

        ExercisePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseRepetition()
    {
        repetitions++;
        RepetitionsText.text = repetitions.ToString();

        if (scoreText != null)
        {
            scoreText.text = repetitions.ToString();
        }      
    }

    public void OnShowExerciseButton()
    {
        ExercisePanel.SetActive(true);

        int clipNo = SceneManager.GetActiveScene().buildIndex + 7;
        if (MenuManager.IS_MUTE)
        {
            voiceManager.UnmuteVoice();
        }
        voiceManager.PlayVoice(clipNo); // clip 8 = 9.Neck Tilting
        StartCoroutine(CloseExercisePanel(clipNo));
    }

    IEnumerator CloseExercisePanel(int clipNo)
    {
        float clipLength = -1;

        if (Language.isEnglish)
        {
            clipLength = voiceManager.EN_Clip[clipNo].length;
        }
        else
        {
            clipLength = voiceManager.EL_Clip[clipNo].length;
        }

        yield return new WaitForSeconds(clipLength);
        ExercisePanel.SetActive(false);


        if (MenuManager.IS_MUTE)
        {
            voiceManager.MuteVoice();
        }
    }

    public void CloseExerciseMenuPanelInstantly()
    {
        ExercisePanel.SetActive(false); // Close NeckTurningPanel Explanation.
        voiceManager.StopVoice();
    }

    public void OnSkipExerciseButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == SceneManager.sceneCountInBuildSettings-1) // if you are on the last Scene
        {
            SceneManager.LoadScene(0); // Load the start Menu Scene.
            return;
        }

        SceneManager.LoadScene(++currentScene); // Load next Scene
    }
    public void OnBackButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(--currentScene);
    }

    public IEnumerator NextScene()
    {
        yield return new WaitForSeconds(4f);

        OnSkipExerciseButton();

    }

    public void OnExitButton()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}

