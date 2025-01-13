using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEditor.Tilemaps;
// goober man is dead
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    private float h;
    public float facingDirection = 1;

    // Dash variables
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public int dashStaminaCost = 25;

    // Stamina variables
    public int maxStamina = 100;
    public int currentStamina;
    public float staminaRegenRate = 10f; // Stamina points per second
    private bool isRegeneratingStamina;

    // TextMeshPro reference
    public TextMeshProUGUI staminaText; // Reference to TextMeshPro UI component
    private SpriteRenderer spriteRenderer;


    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDashing;
    private float dashTimeLeft;
    private float lastDashTime;
    private Animator animator;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;

        // Initialize the stamina text
        UpdateStaminaText();

        animator = GetComponent<Animator>();

    }

    void Update()
    {



        // Stamina regeneration
        if (!isDashing && currentStamina < maxStamina && !isRegeneratingStamina)
        {
            StartCoroutine(RegenerateStamina());
        }

        // If dashing, apply dash movement and return
        if (isDashing)
        {
            Dash();
            return;
        }

        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        Move(new Vector2(moveInput, 0));

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump(jumpForce);
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && CanDash())
        {
            StartDash(moveInput);
        }

        // Update facing direction
        if (moveInput > 0)
        {
            facingDirection = 1;
        }
        else if (moveInput < 0)
        {
            facingDirection = -1;
        }

        // Update animator parameter
        animator.SetFloat("FacingDirection", facingDirection);

    }
    private void FixedUpdate()
    {
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    public void Move(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }

    public void Jump(float jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetBool("isJumping", !isGrounded);
    }

    private void Dash()
    {
        if (dashTimeLeft > 0)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashSpeed, rb.velocity.y);
            dashTimeLeft -= Time.deltaTime;
            animator.SetBool("isDashing", true);
        }
        else
        {
            isDashing = false;
            animator.SetBool("isDashing", false);
        }
    }

    private void StartDash(float moveInput)
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        lastDashTime = Time.time;

        // Deduct stamina
        currentStamina -= dashStaminaCost;
        UpdateStaminaText(); // Update stamina text

        // Face the dash direction
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }
    }

    private bool CanDash()
    {
        return Time.time >= lastDashTime + dashCooldown && currentStamina >= dashStaminaCost;
    }

    private IEnumerator RegenerateStamina()
    {
        isRegeneratingStamina = true;
        while (currentStamina < maxStamina)
        {
            currentStamina += Mathf.RoundToInt(staminaRegenRate * Time.deltaTime);
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
            UpdateStaminaText(); // Update stamina text
            yield return null;
        }
        isRegeneratingStamina = false;
    }

    private void UpdateStaminaText()
    {
        if (staminaText != null)
        {
            staminaText.text = $"Stamina: {currentStamina}/{maxStamina}";
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", !isGrounded);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    //i'm gonna kill someone
}