using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rideable : Interactable
{
    public Player player;
    private Rigidbody2D rb;
    private Rigidbody2D playerRigidBody;
    private bool isOnChair = false;
    private bool isMoving = false;
    private Vector2 targetVelocity = new Vector2(0,0);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRigidBody = player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && isOnChair)
        {
            isMoving = !isMoving;
            targetVelocity = new Vector2(1, 0);
        } 

        if (isMoving)
        {
            playerRigidBody.velocity = targetVelocity;
            this.transform.position = player.GetPosition();
        }
    }

    public override void Interact()
    {
        player.transform.position = this.transform.position;
        Debug.Log("Got into the seat!");

        isOnChair = !isOnChair;
        
        if (isOnChair)
        {
            player.canMove = false;
        } else
        {
            player.canMove = true;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<Player>().OpenInteractableIcon();

        Rideable rideableSeat = collision.GetComponent<Rideable>();
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<Player>().OpenInteractableIcon();

        Rideable rideableSeat= collision.GetComponent<Rideable>();
        isOnChair= false;
    }

    public override string ShowHoverMessage()
    {
        return "Get on the spot";
    }
}
