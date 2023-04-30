using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxJumpHeight = 20f;
    public float jumpChargeSpeed = 10f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float jumpCharge;
    private bool isJumping;
    private bool isColliding; // Variable para saber si el jugador est� colisionando

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = 0;

        if (IsGrounded() && !isJumping && !isColliding) // Solo permitir el movimiento si el jugador no est� saltando y no est� colisionando
        {
            moveX = Input.GetAxis("Horizontal");
        }

        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded() && !isColliding)
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
