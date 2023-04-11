using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest
{
    public bool isActive;

    public string title;
    public string description;
    public int experienceReward;
    public int goldReward;

    public QuestGoal goal;


    public void Complete()
    {
        isActive = false;
        Debug.Log("was completed");
    }
}
