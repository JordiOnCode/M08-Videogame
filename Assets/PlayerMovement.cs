using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour, Controls.IPlayerActions
{
    public float moveSpeed = 6f;
    public float maxJumpHeight = 20f;
    public float jumpChargeSpeed = 30f;
    public float maxFallSpeed = -15f;

    [SerializeField] private LayerMask jumpableGround;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private float jumpCharge;
    private bool isJumping;
    private float moveX;
    private float jumpStartTime;

    private bool isJumpingAnimation, isRunning;

    [SerializeField] private AudioSource jumpSoundEffect, runSoundEffect;
    [SerializeField] private AudioSource shortLandingSoundEffect, longLandingSoundEffect;

    private Controls controls; // Variable para almacenar las acciones de entrada

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        runSoundEffect.loop = true;
    }

    void OnEnable()
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    void Update()
    {
        if (IsGrounded() && !isJumping)
        {
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);            
        }
        else if (isJumping)
        {            
            float jumpDuration = Time.time - jumpStartTime;
            jumpCharge = Mathf.Min(jumpDuration * jumpChargeSpeed, maxJumpHeight);
            // Solo cambia la dirección, no se mueve
            if (moveX != 0) rb.velocity = new Vector2(Mathf.Sign(moveX) * Mathf.Epsilon, rb.velocity.y);
        }

        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }

        UpdateAnimation();
        UpdateSounds();
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveX = context.ReadValue<Vector2>().x;        
    }

    private float jumpEndTime;
    private bool hasLanded;
    private bool wasInAir;

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {            
            isJumping = true;
            jumpStartTime = Time.time;
            hasLanded = false;
            wasInAir = false;
            rb.velocity = Vector2.zero; // Detener al personaje cuando comienza a cargar el salto
        }

        if (context.phase == InputActionPhase.Canceled && isJumping)
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpCharge);
            isJumping = false;
            wasInAir = true;
            jumpEndTime = Time.time;
        }
    }

    private void UpdateAnimation()
    {
        if (isJumpingAnimation)
        {
            anim.SetBool("running", false);
            anim.SetBool("press_Jump", true);
            runSoundEffect.Stop();
        }
        else
        {
            if (moveX > 0f)
            {
                anim.SetBool("running", true);
                sprite.flipX = true;
            }
            else if (moveX < 0f)
            {
                anim.SetBool("running", true);
                sprite.flipX = false;
            }
            else
            {
                anim.SetBool("running", false);
            }
            anim.SetBool("press_Jump", false);
        }

        if (controls.Player.Jump.ReadValue<float>() > 0f)
        {
            isJumpingAnimation = true;
        }
        else
        {
            isJumpingAnimation = false;
        }

        if (isRunning && moveX == 0f)
        {
            isRunning = false;
            runSoundEffect.Stop();
        }
        else if (!isRunning && moveX != 0f && IsGrounded())
        {
            isRunning = true;
            runSoundEffect.Play();
        }
    }

    private void UpdateSounds()
    {
        if (IsGrounded() && wasInAir && !hasLanded)
        {
            jumpEndTime = Time.time; // Actualizamos el tiempo de finalización del salto cuando aterriza

            // Si el tiempo de salto fue más de 2 segundos, reproduce el sonido de aterrizaje largo
            if (jumpEndTime - jumpStartTime >= 2f)
            {
                longLandingSoundEffect.Play();
            }
            // De lo contrario, reproduce el sonido de aterrizaje corto
            else
            {
                shortLandingSoundEffect.Play();
            }

            hasLanded = true; // Indicamos que el sonido de aterrizaje ya se ha reproducido
            wasInAir = false; // Resetear el flag si el personaje ha aterrizado
        }
        else if (!IsGrounded())
        {
            hasLanded = false; // Resetear el flag si el personaje no está en el suelo
            if (!wasInAir) // Si no estábamos en el aire ya, marcamos el inicio del salto
            {
                jumpStartTime = Time.time;
            }
            wasInAir = true; // Marcar que el personaje estaba en el aire
        }
    }
}

