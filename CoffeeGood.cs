using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class CoffeeGood
{
    public enum CoffeeGoodType
    {
        Espresso,
        Latte
    }
    public CoffeeGood(CoffeeGoodType itemTypeVar, int amount, string description)
    {
        coffeeGoodType = itemTypeVar;
        Amount = amount;
        Description = description;
    }

    public string Description;
    public int Amount;
    public CoffeeGoodType coffeeGoodType;
    public int CookingTime = 200;
    public int price = 50;

    public Sprite GetSprite()
    {
        _ = ItemAssets.Instance;
        switch (coffeeGoodType)
        {
            default:
            case CoffeeGoodType.Latte: return ItemAssets.Instance.foodCroissantSprite;
        }
    }

    public bool IsStackable()
    {
        switch (coffeeGoodType)
        {
            default:
            case CoffeeGoodType.Latte:
                return false;
        }
    }
}