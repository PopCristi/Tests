using System;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class Skill
{
    public Text proeficiency;

    public SkillType SkillType;
    public int Progress;
    public int Level;

    public Skill(SkillType skillType, int progress, int level)
    {
        SkillType = skillType;
        Progress = progress;
        Level = level;
    }

    public void LearnSkill()
    {
        Progress += 1;
        Debug.Log("Just improved skill" + SkillType + "," + "new progress is :" + Progress);
        proeficiency.text = SkillType.ToString() + Progress;
    }
}

public enum SkillType
{
    Electricity,
    Mechanical
}


