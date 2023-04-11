using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class CoffeeGoodWorld : MonoBehaviour
{

    public static CoffeeGoodWorld SpawnCoffeeWorld(Vector3 position, CoffeeGood coffeeGood)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfCoffeeGoodWorld, position, Quaternion.identity);

        CoffeeGoodWorld coffeeGoodWorld = transform.GetComponent<CoffeeGoodWorld>();
        coffeeGoodWorld.SetCoffeeGood(coffeeGood);

        return coffeeGoodWorld;
    }

    public CoffeeGood coffeeGood;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;
    private Player player;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();

    }



    public void SetCoffeeGood(CoffeeGood item)
    {
        this.coffeeGood = item;
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

    public CoffeeGood GetItem()
    {
        return coffeeGood;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public static CoffeeGoodWorld DropItem(Vector3 dropPosition, CoffeeGood item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        CoffeeGoodWorld coffeeGoodWorld = SpawnCoffeeWorld(dropPosition + randomDir * 5f, item);
        coffeeGoodWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 5f, ForceMode2D.Impulse);
        return coffeeGoodWorld;
    }
}
