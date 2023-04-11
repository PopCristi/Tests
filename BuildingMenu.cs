using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    public GameObject buildingMenuUI;

    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    public Player player;
    private bool waitingForPlacement = false;
    private Transform builtItemTransform;
    private Vector2 placingPosition;

    // Start is called before the first frame update
    void Start()
    {
        itemSlotContainer = buildingMenuUI.transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        builtItemTransform = transform;
        InitiateBuildingMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingForPlacement)
        {
            Debug.Log("Waiting for placement");
            placingPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
            builtItemTransform.position = placingPosition;
            Debug.Log("We are not at position: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                waitingForPlacement = false;
            }
        }
    }

    public void InitiateBuildingMenu()
    {
        int itemSlotCellSize = 30;
        int x = 0;
        int y = 0;

        RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
        itemSlotRectTransform.gameObject.SetActive(true);

        itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            Debug.Log("Can be crafted");
            List<Item> itemList = player.backPack.inventory.GetItemList();
            foreach(Item item in itemList.ToList())
            {
                Debug.Log(item.itemType.ToString());
                if(item.itemType == Item.ItemType.Food)
                {
                    player.backPack.inventory.RemoveItem(item);
                    continue;
                }
            }

            buildingMenuUI.SetActive(false);
            waitingForPlacement = true;
            Debug.Log("This is before placing the crate:" + Camera.main.ScreenToWorldPoint(Input.mousePosition));
            builtItemTransform = Instantiate(ItemAssets.Instance.pfCrateWorld, new Vector2(0,0), Quaternion.identity);
            builtItemTransform.transform.position = new Vector3(0, 0, 0);
        };

        itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
        {
        };

        itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
        Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
        //player.backPack.RefreshInventoryItems();
        //image.sprite = item.GetSprite();

        //TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
        //if (item.Amount > 1)
        //{
        //    uiText.SetText(item.Amount.ToString());
        //}
        //else
        //{
        //    uiText.SetText("");
        //}
        x++;
        if (x > 4)
        {
            x = 0;
            y++;
        }
    }
}
