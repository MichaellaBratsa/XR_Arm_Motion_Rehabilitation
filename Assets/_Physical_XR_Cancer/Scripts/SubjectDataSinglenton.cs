using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SubjectDataSinglenton : MonoBehaviour
{
    public static SubjectDataSinglenton instance;

    public SubjectData sData;

    // Start is called before the first frame update
    void Awake()
    {
        // Singlenton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        // Singlenton

        //saveInLogFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveInLogFile()
    {
        string path = Application.dataPath + "/../" + "/STATS/";
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Application.persistentDataPath + "/../" + "/STATS/";
#endif

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        print(path);
        string output = "";

        // (!!! IMPORTANT_NOTICE !!!) Special Characters of time cant be part of the name of the .csv File 
        string timeStamp = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy_HH-mm-ss");


        string title = "repetitions, qualityOfLifeLevel, stressLevel, prePainLevel, preFatigueLevel, postShoulderPainLevel, postShoulderFatigueLevel, " +
                        "postArmsPainLevel, postArmsFatigueLevel, postElbowPainLevel, postElbowFatigueLevel";

        output = /*sData.subjectNo + "," +*/ sData.repetitions + "," + sData.qualityOfLifeLevel + "," + sData.stressLevel + "," +
                     sData.prePainLevel + "," + sData.preFatigueLevel + "," + sData.postShoulderPainLevel + "," + sData.postShoulderFatigueLevel + "," +
                     sData.postArmsPainLevel + "," + sData.postArmsFatigueLevel + "," + sData.postElbowPainLevel + "," + sData.postElbowFatigueLevel;

        /*if (File.Exists(path + timeStamp + ".csv"))
        {
            File.AppendAllText((path + timeStamp + ".csv"), output + "\n");

            print("Exists");
        }
        else
        {
            StreamWriter writer = new StreamWriter(path + timeStamp + ".csv");
            writer.Flush();
            writer.WriteLine(title + "\n" + output);
            writer.Close();
            print("Not");
        }*/

        StreamWriter writer = new StreamWriter(path + timeStamp + ".csv");
        writer.Flush();
        writer.WriteLine(title + "\n" + output);
        writer.Close();
        print("Not");

        Debug.Log("saveInLogFile()");

        sData = new SubjectData();
    }
}
