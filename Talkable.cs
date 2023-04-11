using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : Interactable
{
    public QuestGiver questGiver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        Debug.Log("Accepted quest");
        questGiver.OpenQuestWindow();
    }

    public override string ShowHoverMessage()
    {
        return "Wtf is this actually it?";
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().OpenInteractableIcon();
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().CloseInteractableIcon();
        }
    }
}
