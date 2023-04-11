using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Caryable : MonoBehaviour,IInteractable
{

    public Player player;
    public NPCCafeAI npc;

    private bool isInHands = false;
    private bool isInPlacingRange = false;
    private Vector3 placingPosition = Vector3.zero;
    private CaryablePlacingSpot crbPSpot = null;
    public bool playerIsCarrying = false;
    private CrateWorld crateWorld = null;
    private bool interactedWithTheClient = false;


    public string InteractionPrompt => "Give the client the stuff";

    //public override void Interact(out bool interactedWithClient)
    //{
    //    interactedWithClient = false;
    //    if (this.tag == "BakedGood")
    //    {
    //        foreach( RaycastHit2D hit in Physics2D.RaycastAll(this.transform.position, new Vector2(1f, 1f)))
    //        {
    //            if (hit.collider.tag == "Client" && hit.collider.GetComponent<Client>().order.bakedGoods.Any(bg => bg.bakedGoodType == BakedGood.BakedGoodType.Croissant))
    //            {
    //                Debug.LogWarning("The stuff has hit Client");
    //                Destroy(this.gameObject);
    //                hit.collider.GetComponent<Client>().ProcessOrderProgression(GetComponent<BakedGoodWorld>().bakedGood);
    //                interactedWithClient = true;
    //                return;
    //            }
    //        };
    //    }

    //    if (!isInHands)
    //        Debug.Log("Picked up");
    //    else
    //        Debug.Log("Dropped it");

    //    isInHands = !isInHands;

    //    if (isInPlacingRange && crbPSpot.caryable == null)
    //    {
    //        crateWorld = this.transform.GetComponent<CrateWorld>();
    //        this.transform.position = placingPosition;
    //        crbPSpot.caryable = this;
    //        if(player.quest.goal.goalType == GoalType.Fix && player.quest.isActive)
    //        {
    //            bool truth = crateWorld.GetItem().crateType == Crate.CrateType.Oranges;
    //            player.quest.goal.currentAmount++;
    //            if (player.quest.goal.IsReached())
    //            {
    //                player.quest.Complete();
    //            }
    //            Debug.Log("Yessir, am ajuns sa punem cutia cu oranges pentru misiune, hehehe");
    //        }
    //    }
    //    else if (crbPSpot != null)
    //    {
    //        if(crbPSpot.caryable != null)
    //        {
    //            crbPSpot.caryable = null;
    //        }
    //    }
    //}

    public void CarryAnimationContinues()
    {
        if (playerIsCarrying)
            this.transform.position = player.GetPosition();
        else
            this.transform.position = npc.GetPosition();
    }

    public void Update()
    {
        if (isInHands)
            CarryAnimationContinues();
    }

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public bool Interact(Interactor interactor)
    {
        if(interactor.name == "Player")
        {
            var player = interactor.GetComponent<Player>();
            player.carriedObject = GetComponent<BakedGoodWorld>();
        }
        Debug.LogWarning(InteractionPrompt);
        playerIsCarrying = !playerIsCarrying;
        isInHands = !isInHands;
        return true;
    }
}
