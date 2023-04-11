using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance {get; set;}

    private void Awake()
    {
        Instance = this;
    }

    public void Instantiate()
    {
        Instance = this;
    }

    public Transform pfItemWorld;
    public Transform pfCrateWorld;
    public Transform pfBakedGoodWorld;
    public Transform pfClient;
    public Transform pfCoffeeGoodWorld;

    public Sprite foodSprite;
    public Sprite foodCroissantSprite;
    public Sprite stuffSprite;
    public Sprite healthSprite;
    public Sprite orangeCrateSprite;
    //public Sprite coinSprite;
    //public Sprite medkitSprite;

}
