using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] static public float debuffMoveSpeed;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera cam;

    Vector2 mousePos;
    Vector2 movement;

    private float activeMoveSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashLength = .5f, dashCooldown = 1f;

    [SerializeField] private float dashCounter;
    [SerializeField] private float dashCoolCounter;

    [SerializeField] private bool dash = false;

    private void Start()
    {
        activeMoveSpeed = moveSpeed;
        debuffMoveSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //moveSpeed = activeMoveSpeed; // temp
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetKeyDown(KeyCode.Space) && dashCoolCounter <= 0)
        {
            dash = true;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * activeMoveSpeed * debuffMoveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        if(dash)
        {
            if(dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                dash = false;
            }
        }
        if(dashCounter > 0)
        {
            dashCounter -= Time.fixedDeltaTime;

            if(dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }
        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.fixedDeltaTime;
        }
    }
    public void IncreaseSpeed(float speed)
    {
        moveSpeed += speed;
        gameObject.SetActive(true);
    }

    public void dashRangeBuff(float dashbuff)
    {
        dashSpeed += dashbuff;
        gameObject.SetActive(true);
    }
}
