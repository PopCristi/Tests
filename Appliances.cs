using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appliances : MonoBehaviour, IInteractable
{
    public int finishingTime = 0;
    public string InteractionPrompt => "Appliance";

    public bool Interact(Interactor interactor)
    {
        this.finishingTime += 1;
        if (this.finishingTime >= 200)
        {
            Debug.Log("Finished preparing");
            this.finishingTime = 0;
            var plm = CoffeeGoodWorld.SpawnCoffeeWorld(Vector2.zero, new CoffeeGood(CoffeeGood.CoffeeGoodType.Espresso, 1, ""));
        }
        Debug.Log(this.name + " " + this.finishingTime);
        //Debug.Log(InteractionPrompt);
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
