using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrateWorld : MonoBehaviour
{
    public static CrateWorld SpawnCrateWorld(Vector3 position, Crate crate)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfCrateWorld, position, Quaternion.identity);

        CrateWorld crateWorld = transform.GetComponent<CrateWorld>();
        crateWorld.SetItem(crate);

        return crateWorld;
    }

    [SerializeField]
    private Crate crate;
    private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    public void SetItem(Crate crate)
    {
        this.crate = crate;
        spriteRenderer.sprite = crate.GetSprite();
    }

    public Crate GetItem()
    {
        return crate;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    //public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    //{
    //    Vector3 randomDir = UtilsClass.GetRandomDir();
    //    ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 5f, item);
    //    itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 5f, ForceMode2D.Impulse);
    //    return itemWorld;
    //}
}
