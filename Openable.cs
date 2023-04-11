using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Openable : Interactable
{
    public Sprite open;
    public Sprite close;
    public string hoverMessage;
    private SpriteRenderer sr;

    private bool isOpen;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = close;
    }

    private void Update()
    {

    }

    public override void Interact()
    {
        if (isOpen)
        {
            sr.sprite = close;
        } else
        {
            sr.sprite = open;
        }

        isOpen = !isOpen;
    }

    public override string ShowHoverMessage()
    {
        return hoverMessage;    
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
