using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [Header("Settings")]
    public float movementSpeed = 5f;
    public float jumpForce = 10f;
    public float maxJumpTime = 0.5f;
    public float jumpHoldForce = 2.5f;
    private bool isGrounded;
    private float jumpTimeCounter;
    public bool isJumping;

    [Header("Objects")]
    public GameObject player;
    public Rigidbody2D rb;
    public LayerMask groundLayer;
    public GameObject sprite;

    // Update is called once per frame
    void Update()
    {
        float XInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetAxisRaw("Horizontal") != 0)
            sprite.transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
        
        rb.velocity = new Vector2(XInput * movementSpeed, rb.velocity.y);

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHoldForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

    }
}

