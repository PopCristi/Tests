using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType { 
        Health,
        Food,
        Stuff
    }
    public Item(ItemType itemTypeVar, int amount, string description)
    {
        itemType = itemTypeVar;
        Amount = amount;
        Description = description;
    }

    public string Description;
    public int Amount;
    public ItemType itemType;

    public Sprite GetSprite()
    {
        _ = ItemAssets.Instance;
        switch (itemType)
        {
            default:
            case ItemType.Food: return ItemAssets.Instance.foodSprite;
            case ItemType.Stuff: return ItemAssets.Instance.stuffSprite;
            case ItemType.Health: return ItemAssets.Instance.healthSprite;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Food:
            case ItemType.Health:
                return true;
            case ItemType.Stuff:
                return false;

        }

    }
}
