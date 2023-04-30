using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxJumpHeight = 10f;
    public float jumpChargeSpeed = 5f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float jumpCharge;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = 0;

        if (IsGrounded() && !isJumping)
        {
            moveX = Input.GetAxis("Horizontal");
        }

        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            isJumping = true;
            jumpCharge = 0;
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            jumpCharge += Time.deltaTime * jumpChargeSpeed;
            jumpCharge = Mathf.Min(jumpCharge, maxJumpHeight);
        }

        if (Input.GetButtonUp("Jump") && isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpCharge);
            isJumping = false;
        }
    }

    bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(rb.position, Vector2.down, rb.GetComponent<Collider2D>().bounds.extents.y + extraHeight, groundLayer);
        return raycastHit.collider != null;
    }
}

