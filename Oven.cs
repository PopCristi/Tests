using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Oven : Interactable
{

    public bool hasSomethingInside;
    public bool canBePickedUp = false;
    private bool haveBakedGoodInHand = false;
    private BakedGoodWorld bakedGoodsTransform;
    private System.Timers.Timer timer;
    private int milliseconds = 0;
    private int stoppingTime = 0;


    public override void Interact()
    {
        Debug.Log("Interacted");

        if (!hasSomethingInside & haveBakedGoodInHand)
        {
            Destroy(bakedGoodsTransform.gameObject);
            StartCooking();
        }

        if (canBePickedUp)
        {
            HandleDoneCooking();
            bakedGoodsTransform.GetComponent<Caryable>().playerIsCarrying = true;
            bakedGoodsTransform.GetComponent<Caryable>().Interact(new Interactor());
            canBePickedUp = false;
            hasSomethingInside = false;
            bakedGoodsTransform = null;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player") {
            Debug.Log("Player entered the oven zone");
        }

        if (collision.tag == "BakedGood")
        {
            haveBakedGoodInHand = true;
            bakedGoodsTransform = collision.gameObject.GetComponent<BakedGoodWorld>();
            Debug.Log("Cake entered the oven zone");
        }

        Debug.Log("Entered oven zone");
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "BakedGood")
        {
            Debug.Log("Cake left the zone");
            haveBakedGoodInHand = false;
        }
        Debug.Log("Exited oven zone");
    }

    public override string ShowHoverMessage()
    {
        return "Oven can be interacted with";
    }

    private void StartCooking() 
    {
        if (bakedGoodsTransform is null) 
        {
            return;
        }
        Debug.Log("Started cooking");

        stoppingTime = bakedGoodsTransform.GetComponent<BakedGoodWorld>().bakedGood.CookingTime;
        timer = new System.Timers.Timer(1);
        timer.Elapsed +=  (sender, e) =>  HandleTimer();
        timer.Interval = 1;
        timer.Start();
        hasSomethingInside = true;
    }

    private void HandleTimer()
    {
        milliseconds++;
    }

    private void HandleDoneCooking()
    {
        bakedGoodsTransform = BakedGoodWorld.SpawnBakedWorld(new Vector2(-2.18f, 3.2f), new BakedGood(BakedGood.BakedGoodType.Croissant, 1, ""));
    }

    public void Update()
    {
        if (timer != null)
        {
            if (milliseconds >= stoppingTime && timer.Enabled == true)
            {
                timer.Stop();
                timer.Dispose();
                canBePickedUp = true;
                milliseconds = 0;
                Debug.LogWarning("It has done cooking can be picked up");
            }
        }
    }
}
