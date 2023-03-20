using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] static public float debuffMoveSpeed;

    [SerializeField] private Rigidbody2D rb;
    //[SerializeField]
    private Camera cam;

    Vector2 mousePos;
    Vector2 movement;

    private float activeMoveSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashLength = .5f, dashCooldown = 1f;

    [SerializeField] private float dashCounter;
    [SerializeField] private float dashCoolCounter;

    [SerializeField] private bool dash = false;

    // Get the horizontal and vertical input values based on the player number and input mode
    private float horizontalInput = 0f;
    private float verticalInput = 0f;

    private void Start()
    {
        cam = Camera.main;
        activeMoveSpeed = moveSpeed;
        debuffMoveSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //moveSpeed = activeMoveSpeed; // temp
        if (!IsOwner) return;

        horizontalInput = Input.GetAxisRaw("HorizontalPlayer1");
        verticalInput = Input.GetAxisRaw("VerticalPlayer1");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space) && dashCoolCounter <= 0) // If the dash cooldown is less down or equal to 0 then dash is available/usable
        {
            dash = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        transform.position += movement.normalized * activeMoveSpeed * debuffMoveSpeed * Time.fixedDeltaTime;

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
