using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageTextUpdater : MonoBehaviour
{   
    Language lang;

    // Start is called before the first frame update
    void Start()
    {
        ChangeLanguage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLanguage()
    {
        lang = GameObject.Find("Language").GetComponent<Language>();

        for (int i = 0; i < lang.locListHandler.textsArray.Length; i++)
        {
            if (gameObject.name == lang.locListHandler.textsArray[i].ID_Name)
            {
                if (Language.isEnglish)
                {
                    GetComponent<TMP_Text>().text = lang.locListHandler.textsArray[i].EnText;
                }
                else
                {
                    GetComponent<TMP_Text>().text = lang.locListHandler.textsArray[i].ElText;
                }
            }
        }
    }
}
