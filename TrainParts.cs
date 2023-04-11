using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainParts : Caryable
{
    public TrainPartType trainPartType;
    public float durability;
    public bool needsChanging;

    public enum TrainPartType
    {
        Engine,
        Wheels,
        Windows,
        Chimney
    }
}
