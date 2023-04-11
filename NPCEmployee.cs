using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEmployee : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 0.0001f;
    public Player player;
    private Vector2 boxSize = new Vector2(2f, 2f);

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Z))
        //{
        //    PerformAction();
        //}

        if (Input.GetKeyDown(KeyCode.C))
        {
            CheckInteraction();
        }
        //Move(1,0);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Move(float xDir, float yDir)
    {
        float xVal = xDir * speed * 100 * Time.deltaTime;
        float yVal = yDir * speed * 100 * Time.deltaTime;
        Vector2 targetVelocity = new Vector2(xVal, yVal);
        rb.velocity = targetVelocity;

        Vector3 currentScale = transform.localScale;
        //facingRight = this.transform.position.Scale() 

        currentScale = transform.localScale;
    }

    void MoveTo(Vector3 newPosition)
    {
        this.transform.position = newPosition;
    }

    public void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
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

    public void PerformAction()
    {
        MoveTo(new Vector3(-1.647972f, 0.3583069f, 0f));
        CheckInteraction();
        MoveTo(new Vector3(0.98f, -2.43f, 0f));
        CheckInteraction();
        //MoveTo(new Vector3(-1.647972f, 0.3583069f, 0f));
    }
}
