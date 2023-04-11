using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class BakedGood
{
    public enum BakedGoodType
    {
        Croissant,
        Bagguete
    }
    public BakedGood(BakedGoodType itemTypeVar, int amount, string description)
    {
        bakedGoodType = itemTypeVar;
        Amount = amount;
        Description = description;
    }

    public string Description;
    public int Amount;
    public BakedGoodType bakedGoodType;
    public int CookingTime = 10000;
    public int price = 50;

    public Sprite GetSprite()
    {
        _ = ItemAssets.Instance;
        switch (bakedGoodType)
        {
            default:
            case BakedGoodType.Croissant: return ItemAssets.Instance.foodCroissantSprite;
        }
    }

    public bool IsStackable()
    {
        switch (bakedGoodType)
        {
            default:
            case BakedGoodType.Croissant:
                return false;
        }
    }
}