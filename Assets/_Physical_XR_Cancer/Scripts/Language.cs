using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Language : MonoBehaviour
{
    /***********************************************************************************************
    *   Helping Assets
    *   --------------
    *   
    *     https://assetstore.unity.com/packages/tools/gui/simple-zoom-143625 // Simple Zoom Asset Store
    *     https://csvjson.com/csv2json // .csv to .json Convertor Online
    *     https://www.convertcsv.com/csv-to-json.htm?utm_content=cmp-true // .csv to .json Convertor Online
    *     https://json2csharp.com/ // .json to .C# Class Convertor Online
    *     
    *     https://www.youtube.com/watch?v=tI9NEm02EuE // CSV Reader Unity Tutorial
    * 
    ***********************************************************************************************/ 


    readonly int CSV_COLUMNS_SIZE = 3;
    public static bool isEnglish = false;

    [Tooltip("Update the LocalizationTexts.csv file and attach the LanguageManager.cs script on the corresponding TMP_Text.")]
    public TextAsset textAssetData;
    public LocalizationList locListHandler = new LocalizationList();


    public MenuManager menuManager;
    public VoiceManager voiceManager;

    // Start is called before the first frame update
    void Awake()
    {
        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.GetString("isEnglish") != "")
        {
            isEnglish = bool.Parse(PlayerPrefs.GetString("isEnglish"));
        }
        print(PlayerPrefs.GetString("isEnglish"));

        ReadCSV();

        /*if (menuManager!= null)
        {
            menuManager.CloseAllPanels();
            menuManager.EnableCurrentPanel();
        }*/        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / CSV_COLUMNS_SIZE - 1;
        locListHandler.textsArray = new Localization[tableSize];

        for (int i = 0; i < tableSize; i++)

        {
            locListHandler.textsArray[i] = new Localization();

            locListHandler.textsArray[i].ID_Name = data[CSV_COLUMNS_SIZE * (i + 1)];
            locListHandler.textsArray[i].EnText = data[CSV_COLUMNS_SIZE * (i + 1) + 1];
            locListHandler.textsArray[i].ElText = data[CSV_COLUMNS_SIZE * (i + 1) + 2];
        }
    }

    public void OnEnButton()
    {
        isEnglish = true;
        PlayerPrefs.SetString("isEnglish", "True");

        voiceManager.PlayVoice();

        menuManager.currentActivePanel++;
        menuManager.CloseAllPanels();
        menuManager.EnableCurrentPanel();
    }

    public void OnElButton()
    {
        isEnglish = false;
        PlayerPrefs.SetString("isEnglish", "False");

        voiceManager.PlayVoice();

        menuManager.currentActivePanel++;
        menuManager.CloseAllPanels();
        menuManager.EnableCurrentPanel();
    }
}



[Serializable]
public class Localization
{
    public string ID_Name;
    public string EnText;
    public string ElText;
}

[Serializable]
public class LocalizationList
{
    public Localization[] textsArray;
}