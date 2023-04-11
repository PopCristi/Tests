using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    private List<Item> itemList;
    private Action<Item> useItemAction;

    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInvetory = false;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.Amount += item.Amount;
                    itemAlreadyInInvetory = true;
                }
            }
            if (!itemAlreadyInInvetory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        Item itemInInventory = null;
        foreach (Item inventoryItem in itemList)
        {
            if (inventoryItem.itemType == item.itemType)
            {
                inventoryItem.Amount -= item.Amount;
                itemInInventory = inventoryItem;
            }
        }
        if (itemInInventory != null && itemInInventory.Amount <= 0)
        {
           itemList.Remove(itemInInventory);
        }
        else
        {
            item.Amount --;
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
