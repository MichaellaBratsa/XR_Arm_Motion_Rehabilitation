using System;
using System.Collections.Generic;



[Serializable] // This makes the class able to be serialized into a JSON
public class SubjectData
{
    public string subjectNo;
    public int repetitions;
    public int qualityOfLifeLevel;
    public int stressLevel;
    public int prePainLevel;
    public int preFatigueLevel;

    public int postShoulderPainLevel;
    public int postShoulderFatigueLevel;
    public int postArmsPainLevel;
    public int postArmsFatigueLevel;
    public int postElbowPainLevel;
    public int postElbowFatigueLevel;


    public SubjectData()
    {
        subjectNo = "-1";
        repetitions = -1;
        qualityOfLifeLevel = -1;
        stressLevel = -1;
        prePainLevel = -1;
        preFatigueLevel = -1;

        postShoulderPainLevel = -1;
        postShoulderFatigueLevel = -1;
        postArmsPainLevel = -1;
        postArmsFatigueLevel = -1;
        postElbowPainLevel = -1;
        postElbowFatigueLevel = -1;
    }

}


