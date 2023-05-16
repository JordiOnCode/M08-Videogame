using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float maxJumpHeight = 20f;
    public float jumpChargeSpeed = 30f;

    // Nueva variable para la velocidad máxima de caída.
    public float maxFallSpeed = -15f;

    [SerializeField] private LayerMask jumpableGround;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private float jumpCharge;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        if (IsGrounded() && !isJumping)
        {
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        }
        else if (isJumping)
        {
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        }

        // Verificar y limitar la velocidad de caída.
        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }

        if (Input.GetButton("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }


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
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
