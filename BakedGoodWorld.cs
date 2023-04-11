using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class BakedGoodWorld : MonoBehaviour
{

    public static BakedGoodWorld SpawnBakedWorld(Vector3 position, BakedGood bakedGood)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfBakedGoodWorld, position, Quaternion.identity);

        BakedGoodWorld bakedGoodWorld = transform.GetComponent<BakedGoodWorld>();
        bakedGoodWorld.SetBakedGood(bakedGood);

        return bakedGoodWorld;
    }

    public BakedGood bakedGood;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;
    private Player player;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();

    }



    public void SetBakedGood(BakedGood item)
    {
        this.bakedGood = item;
        spriteRenderer.sprite = item.GetSprite();

        if (item.Amount > 1)
        {
            textMeshPro.SetText(item.Amount.ToString());
        }
        else
        {
            textMeshPro.SetText("");
        }
    }

    public BakedGood GetItem()
    {
        return bakedGood;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public static BakedGoodWorld DropItem(Vector3 dropPosition, BakedGood item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        BakedGoodWorld bakedGoodWorld = SpawnBakedWorld(dropPosition + randomDir * 5f, item);
        bakedGoodWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 5f, ForceMode2D.Impulse);
        return bakedGoodWorld;
    }
}
