using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDataPersistence
{

    public float speed = 1;
    //Private
    Rigidbody2D rb;
    float horizontalValue;
    float verticalValue;
    bool facingRight = true;
    public Image img;
    private Inventory inventory;
    public GameObject backpackUI;
    public GameObject buildingMenuUI;
    public bool canMove = true;
    public bool interactedWithClient = false;

    public Quest quest;
    public List<Skill> skills;
    [SerializeField] public BackPack backPack;
    private Vector2 boxSize = new Vector2(0.1f, 1f);

    private bool isBPOpen = false;
    private bool isBuildingMenuOpen = false;
    public BakedGoodWorld carriedObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        img = GameObject.Find("InteractStuff").GetComponent<Image>();
        inventory = new Inventory(UseItem);
        backPack.SetPlayer(this);
        backPack.SetInventory(inventory);
        img.enabled = false;
        inventory.AddItem(new Item(Item.ItemType.Health, 1, ""));
        inventory.AddItem(new Item(Item.ItemType.Food, 1, ""));
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.Stuff:
                Debug.Log("Used Stuff");
                inventory.RemoveItem(new Item(Item.ItemType.Stuff, 1, ""));
                break;
            case Item.ItemType.Health:
                Debug.Log("Used Health");
                inventory.RemoveItem(new Item(Item.ItemType.Health, 1, ""));
                break;
            case Item.ItemType.Food:
                Debug.Log("Used Food");
                inventory.RemoveItem(new Item(Item.ItemType.Food, 1, ""));
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        verticalValue = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.E))
            CheckInteraction();

        if (Input.GetKeyDown(KeyCode.I))
            ShowInventory();

        if (Input.GetKeyDown(KeyCode.X))
            GoBattle();
        if (Input.GetKeyDown(KeyCode.L))
            skills.First().LearnSkill();
        if (Input.GetKeyDown(KeyCode.M))
            skills[1].LearnSkill();

        if (Input.GetKeyDown(KeyCode.Tab))
            ShowBuildingMenu();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            BakedGoodWorld.SpawnBakedWorld( new Vector2(-2.18f, 3.2f), new BakedGood (BakedGood.BakedGoodType.Croissant, 1, ""));
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Transform transform = GameObject.FindGameObjectWithTag("SpawnPointClient").transform;
            GameObject newClient = Instantiate(ItemAssets.Instance.pfClient, transform.position, Quaternion.identity).GameObject();
            Client newCLientComponent = newClient.GetComponent<Client>();
            OrderTableQueueChecker.orderTableClients.Add(newCLientComponent);
        }


        if (canMove)
        {
            Move(horizontalValue, verticalValue);
        }

        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            Text textbox = GameObject.Find("TextBoxMessage").GetComponent<Text>();
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>()){
                    textbox.enabled = true;
                    textbox.transform.position = this.transform.position;
                    textbox.text = rc.transform.GetComponent<Interactable>().ShowHoverMessage();
                    Debug.LogWarning("Have hit " + rc.collider.name);
                    //OpenInteractableIcon();
                } else{
                    textbox.enabled = false;
                    //CloseInteractableIcon();
                }
            }
        }
    }

    void Move(float xDir, float yDir)
    {
        float xVal = xDir * speed * 100 * Time.deltaTime;
        float yVal = yDir * speed * 100 * Time.deltaTime;
        Vector2 targetVelocity = new Vector2(xVal,yVal);
        rb.velocity = targetVelocity;

        Vector3 currentScale = transform.localScale;
        //facingRight = this.transform.position.Scale() 

        //if (facingRight && xDir < 0) 
        //{
        //    transform.localScale = new Vector3(-2, 2, 2);
        //    facingRight = false;
        //} else if (!facingRight){

        //    transform.localScale = new Vector3(2, 2, 2);
        //    facingRight = true;
        //}

        currentScale = transform.localScale;
    }


    public void OpenInteractableIcon()
    {
        img.enabled = true;
        img.transform.position = this.transform.position;
    }

    public void CloseInteractableIcon()
    {
        img.enabled = false;
    }

    public void ShowInventory()
    {
        if (isBPOpen == false)
        {
            backpackUI.SetActive(true);
        }
        else
            backpackUI.SetActive(false);

        isBPOpen = !isBPOpen;
    }

    public void ShowBuildingMenu()
    {
        if (isBuildingMenuOpen == false)
        {
            buildingMenuUI.SetActive(true);
        }
        else
            buildingMenuUI.SetActive(false);

        isBuildingMenuOpen = !isBuildingMenuOpen;
    }

    public void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if(hits.Length > 0)
        {
            foreach(RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    public void GoBattle()
    {
        Debug.Log("Went to battle");
        if (quest.isActive)
        {
            quest.goal.EnemyKilled();
            if (quest.goal.IsReached())
            {
                quest.Complete();
            }
        }
    }

    public void LoadData(GameData data)
    {
        PlayerStatistics.playerMoney = data.money;
        this.transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.money = PlayerStatistics.playerMoney;
        data.playerPosition = this.transform.position;
    }
}
