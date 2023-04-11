using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Crate
{
    public enum CrateType
    {
        Oranges
    }
    public Crate(CrateType crateTypeVar)
    {
        crateType = crateTypeVar;
    }

    public CrateType crateType;

    public Sprite GetSprite()
    {
        _ = ItemAssets.Instance;
        switch (crateType)
        {
            default:
            case CrateType.Oranges: return ItemAssets.Instance.orangeCrateSprite;
        }
    }

    //public bool IsStackable()
    //{
    //    switch (crateType)
    //    {
    //        default:
    //        case ItemType.Food:
    //        case ItemType.Health:
    //            return true;
    //        case ItemType.Stuff:
    //            return false;

    //    }

    //}
}
