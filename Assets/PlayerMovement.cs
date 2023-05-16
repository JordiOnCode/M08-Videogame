using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float maxJumpHeight = 20f;
    public float jumpChargeSpeed = 30f;

    public float maxFallSpeed = -15f;

    [SerializeField] private LayerMask jumpableGround;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private float jumpCharge;
    private bool isJumping;

    // Instancia de los controles
    private Controls controls;
    private Vector2 move;

    void Awake()
    {
        controls = new Controls();

        controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => move = Vector2.zero;

        controls.Player.Jump.performed += ctx => Jump();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float moveX = move.x;

        if (IsGrounded() && !isJumping)
        {
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        }
        else if (isJumping)
        {
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        }

        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }
    }

    void Jump()
    {
        if (IsGrounded())
        {
            isJumping = true;
            jumpCharge = 0;

            jumpCharge += Time.deltaTime * jumpChargeSpeed;
            jumpCharge = Mathf.Min(jumpCharge, maxJumpHeight);

            rb.velocity = new Vector2(rb.velocity.x, jumpCharge);
            isJumping = false;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
}
