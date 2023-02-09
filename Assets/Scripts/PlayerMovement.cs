using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 mousePos;
    Vector2 movement;

    private float activeMoveSpeed;
    public float dashSpeed;
    public float dashLength = .5f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;

    private bool dash = false;

    private void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetKeyDown(KeyCode.Space) && dashCoolCounter <= 0) // If the dash cooldown is less down or equal to 0 then dash is available/usable
        {
            dash = true;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * activeMoveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        if(dash) // If statements allow the player to dash when having atleast one dashcounter, but setting dash at false when having 0 dashcounters
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
}
