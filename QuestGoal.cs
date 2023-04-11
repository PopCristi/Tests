using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestGoal
{

    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled()
    {
        if (goalType == GoalType.Fix)
            currentAmount++;
    }

    public void ItemCollected()
    {
        if (goalType == GoalType.Gathering)
            currentAmount++;
    }

}

public enum GoalType
{
    Fix,
    Gathering
}