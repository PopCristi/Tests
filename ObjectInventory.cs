using CodeMonkey.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInventory : MonoBehaviour
{
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Inventory inventory;
    public GameObject inventoryUI;
    public Player player;
    public void Start()
    {


        inventory = new Inventory(UseItem);
        inventory.AddItem(new Item(Item.ItemType.Food, 1, ""));
        inventory.AddItem(new Item(Item.ItemType.Stuff, 1, ""));

        Debug.Log(inventory.GetItemList().Count);
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();

    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Stuff:
                Debug.Log("Used Stuff");
                inventory.RemoveItem(new Item(Item.ItemType.Stuff, 1, ""));
                break;
            case Item.ItemType.Health:
                Debug.Log("Used Health");
                inventory.RemoveItem(new Item(Item.ItemType.Health, 1, ""));
                break;
            case Item.ItemType.Food:
                Debug.Log("Used Food");
                inventory.RemoveItem(new Item(Item.ItemType.Food, 1, ""));
                break;
        }
    }

    private void Inventory_OnItemListChanged(object sender, EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        itemSlotContainer = inventoryUI.transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float itemSlotCellSize = 30f;
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {   
            };

            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                //Move to player inventory
                player.backPack.inventory.AddItem(item);
                this.inventory.RemoveItem(item);

            };
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            if (item.Amount > 1)
            {
                uiText.SetText(item.Amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }

        }
    }
}
