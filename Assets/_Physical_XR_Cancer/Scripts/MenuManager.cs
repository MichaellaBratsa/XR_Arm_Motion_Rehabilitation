using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    ///////////////////////// OPEN XR SETUP TUTORIALS /////////////////////////
    /// 
    /// 1) https://www.youtube.com/watch?v=F4gDjCLYX88
    /// 2) https://www.youtube.com/watch?v=d2gEEQeu1b4
    /// 
    /// 3) https://polyhaven.com/
    /// 
    ////////////////////////// OPEN XR SETUP TUTORIALS /////////////////////////

    ////////////////////////////////// TO-DO ///////////////////////////////////
    ///
    /// 1) Test all Exercises [DONE]
    /// 2) 6. Shoulder Adduction (Racket should extend) [DONE]
    /// 4) Set up the Environment [DONE]
    ///     
    /// 3) Test Save in LogFile [DONE]
    /// 5) Record Voice Commands [DONE]
    /// 6) Test in Oculus Quest 2
    /// 
    /// ) Sound Effects
    /// ) Calibration [LATER]
    /// ) VR Keyboard [LATER]
    /// 
    ////////////////////////////////// TO-DO ///////////////////////////////////

    public static bool IS_MUTE;
    public static int TOTAL_REPETITIONS = 10;

    public GameObject [] canvasPanels;
    public int currentActivePanel;

    //public SubjectData sData;

    public Laser laser;

    public VoiceManager voiceManager;
    private SubjectDataSinglenton subjectDataSinglenton;


    // Start is called before the first frame update
    void Awake()
    {
        subjectDataSinglenton = GameObject.Find("SubjectDataSinglenton").GetComponent<SubjectDataSinglenton>();

        int currentScene = SceneManager.GetActiveScene().buildIndex;

        switch (currentScene)
        {
            case 0: // 0. Menu Scene (Neck Exercises)
                currentActivePanel = 0;
                break;
            case 4: // 4. Menu Shoulder (Shoulder Exercises)
                currentActivePanel = 5;
                break;
            case 8: // 8. Menu Arms (Arms Exercises)
                currentActivePanel = 5;
                break;
            case 11: // 11. Menu Elbow (Arms Exercises)
                currentActivePanel = 5;
                break;
            default:
                /////////
                break;
        }
        
        CloseAllPanels();
        EnableCurrentPanel();

        //NeckTiltingPanel.SetActive(NeckTiltingPanelIsOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseAllPanels()
    {
        for (int i=0; i< canvasPanels.Length; i++)
        {
            canvasPanels[i].SetActive(false);
        }
    }
    public void EnableCurrentPanel()
    {
        canvasPanels[currentActivePanel].SetActive(true);
    }

    public void OnStartButton()
    {
        IS_MUTE = true;
        currentActivePanel++;
        CloseAllPanels();
        EnableCurrentPanel();
        voiceManager.PlayVoice();

        voiceManager.MuteVoice();  // Play WITHOUT Voice Commands
    }

    public void OnStartWithCommandsButton()
    {
        IS_MUTE = false;
        currentActivePanel++;
        CloseAllPanels();
        EnableCurrentPanel();
        voiceManager.PlayVoice();

        voiceManager.UnmuteVoice();  // Play with Voice Commands
    }

    public void OnExitButton()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

    private void OpenNextPanel()
    {
        currentActivePanel++;
        CloseAllPanels();
        EnableCurrentPanel();
    }

    public void On10RepButton()
    {
        TOTAL_REPETITIONS = 10;
        subjectDataSinglenton.sData.repetitions = TOTAL_REPETITIONS;
        OpenNextPanel();
        voiceManager.PlayVoice();
    }

    public void On20RepButton()
    {
        TOTAL_REPETITIONS = 20;
        subjectDataSinglenton.sData.repetitions = TOTAL_REPETITIONS;
        OpenNextPanel();
        voiceManager.PlayVoice();
    }

    public void On30RepButton()
    {
        TOTAL_REPETITIONS = 30;
        subjectDataSinglenton.sData.repetitions = TOTAL_REPETITIONS;
        OpenNextPanel();
        voiceManager.PlayVoice();
    }

    public void OnRateQualityLifeButton()
    {
        subjectDataSinglenton.sData.qualityOfLifeLevel = int.Parse(laser.lastGameObjectButtonCalled.GetComponentInChildren<TMP_Text>().text);
        OpenNextPanel();
        voiceManager.PlayVoice();
        print(subjectDataSinglenton.sData.qualityOfLifeLevel);
    }
    public void OnStressLevelButton()
    {
        subjectDataSinglenton.sData.stressLevel = int.Parse(laser.lastGameObjectButtonCalled.GetComponentInChildren<TMP_Text>().text);
        OpenNextPanel();
        voiceManager.PlayVoice();
        print(subjectDataSinglenton.sData.stressLevel);
    }
    public void OnPrePainLevelButton()
    {
        subjectDataSinglenton.sData.prePainLevel = int.Parse(laser.lastGameObjectButtonCalled.name[0].ToString());
        OpenNextPanel();
        voiceManager.PlayVoice();
        print(subjectDataSinglenton.sData.prePainLevel);
    }

    public void OnPreFatigueLevelButton()
    {
        subjectDataSinglenton.sData.preFatigueLevel = int.Parse(laser.lastGameObjectButtonCalled.GetComponentInChildren<TMP_Text>().text);
        OpenNextPanel();
        voiceManager.PlayVoice();
        print(subjectDataSinglenton.sData.preFatigueLevel);
    }

    public void OnPostPainLevelButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        switch (currentScene)
        {
            case 4: // 4. Menu Shoulder (Shoulder Exercises)
                subjectDataSinglenton.sData.postShoulderPainLevel = int.Parse(laser.lastGameObjectButtonCalled.name[0].ToString());
                print(subjectDataSinglenton.sData.postShoulderPainLevel);
                break;
            case 8: // 8. Menu Arms (Arms Exercises)
                subjectDataSinglenton.sData.postArmsPainLevel = int.Parse(laser.lastGameObjectButtonCalled.name[0].ToString());
                print(subjectDataSinglenton.sData.postArmsPainLevel);
                break;
            case 11: // 11. Menu Elbow (Arms Exercises)
                subjectDataSinglenton.sData.postElbowPainLevel = int.Parse(laser.lastGameObjectButtonCalled.name[0].ToString());
                print(subjectDataSinglenton.sData.postElbowPainLevel);
                break;
            default:
                /////////
                break;
        }

        OpenNextPanel();
        voiceManager.PlayVoice();    
    }

    public void OnPostFatigueLevelButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int clipNo = -1;

        switch (currentScene)
        {          
            case 4: // 4. Menu Shoulder (Shoulder Exercises)
                clipNo = 8;
                subjectDataSinglenton.sData.postShoulderFatigueLevel = int.Parse(laser.lastGameObjectButtonCalled.GetComponentInChildren<TMP_Text>().text);
                print(subjectDataSinglenton.sData.postShoulderFatigueLevel);
                break;
            case 8: // 8. Menu Arms (Arms Exercises)
                clipNo = 9;
                subjectDataSinglenton.sData.postArmsFatigueLevel = int.Parse(laser.lastGameObjectButtonCalled.GetComponentInChildren<TMP_Text>().text);
                print(subjectDataSinglenton.sData.postArmsFatigueLevel);
                break;
            case 11: // 11. Menu Elbow (Arms Exercises)
                clipNo = 10;
                subjectDataSinglenton.sData.postElbowFatigueLevel = int.Parse(laser.lastGameObjectButtonCalled.GetComponentInChildren<TMP_Text>().text);
                print(subjectDataSinglenton.sData.postElbowFatigueLevel);
                break;
            default:
                /////////
                break;
        }
      
        OpenNextPanel();
        voiceManager.PlayVoice(clipNo); //Play Shoulder or Arm or Elbow Exercises clip
    }

    public void OnNextExerciseButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++currentScene);
    }

    public void OnSkipALLButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = 0;

        switch (currentScene)
        {
            case 0:     // 0. Menu Scene (Neck Exercises)
                nextScene = 4;
                break;
            case 4:     // 4. Menu Shoulder (Shoulder Exercises)
                nextScene = 8;
                break;
            case 8:     // 8. Menu Arms (Arms Exercises)
                nextScene = 11; 
                break;
            case 11:    // 11. Menu Elbow (Arms Exercises)
                nextScene = 0;
                break;
            default:
                /////////
                break;
        }

        SceneManager.LoadScene(nextScene);
    }

    public void OnBackButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(--currentScene);
    }
    
    /*public void OnShowNeckTiltingButton()
    {
        NeckTiltingPanelIsOn = !NeckTiltingPanelIsOn;
        NeckTiltingPanel.SetActive(NeckTiltingPanelIsOn);
        voiceManager.PlayVoice(8); // clip 8 = 9.Neck Tilting
        //StartCoroutine(EnableShowNeckTiltingButton());
        
    }

    IEnumerator EnableShowNeckTiltingButton()
    {
        ShowNeckTiltingButton.SetActive(false);
        yield return new WaitForSeconds(1f);
        ShowNeckTiltingButton.SetActive(true);
    }*/
}
