using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayerController : MonoBehaviour {

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] static public float debuffMoveSpeed;

    [SerializeField] private Rigidbody2D rb;

    private float activeMoveSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashLength = .5f, dashCooldown = 1f;

    [SerializeField] private float dashCounter;
    [SerializeField] private float dashCoolCounter;

    [SerializeField] private bool dash = false;

    // Get the horizontal and vertical input values based on the player number and input mode
    private float horizontalInput = 0f;
    private float verticalInput = 0f;

    private bool facingRight = true;

    private void Start()
    {
        activeMoveSpeed = moveSpeed;
        debuffMoveSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //moveSpeed = activeMoveSpeed; // temp
        horizontalInput = Input.GetAxisRaw("HorizontalPlayer2");
        verticalInput = Input.GetAxisRaw("VerticalPlayer2");
        if (Input.GetKeyDown(KeyCode.RightControl) && dashCoolCounter <= 0) // If the dash cooldown is less down or equal to 0 then dash is available/usable
        {
            dash = true;
        }

        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        } else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }

    }

    private void FixedUpdate()
    {
        // Move the player horizontally and vertically based on the input values
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        transform.position += movement.normalized * moveSpeed * debuffMoveSpeed * Time.fixedDeltaTime;

        if (dash) // If statements allow the player to dash when having atleast one dashcounter, but setting dash at false when having 0 dashcounters
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0) {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                dash = false;
            }
        }
        if (dashCounter > 0) {
            dashCounter -= Time.fixedDeltaTime;

            if (dashCounter <= 0) {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCooldown;
            }
        }
        if (dashCoolCounter > 0) {
            dashCoolCounter -= Time.fixedDeltaTime;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
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
