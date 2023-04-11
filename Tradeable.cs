using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tradeable : Interactable
{
    public GameObject npcInventory;
    public bool isNpcInventoryOpen = false;
    public override void Interact()
    {
        if (!isNpcInventoryOpen)
            npcInventory.SetActive(true);
        else
            npcInventory.SetActive(false);
        Debug.Log("Can open inventory");
        isNpcInventoryOpen = !isNpcInventoryOpen;
    }


    public override void OnTriggerEnter2D(Collider2D collision)
    {
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
    }

    public override string ShowHoverMessage()
    {
        return "";
    }
}
