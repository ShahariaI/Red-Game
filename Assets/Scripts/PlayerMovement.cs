using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

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

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDashing;
    private float dashTimeLeft;
    private float lastDashTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
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
    }

    public void Move(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }

    public void Jump(float jumpForce)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Dash()
    {
        if (dashTimeLeft > 0)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashSpeed, rb.velocity.y);
            dashTimeLeft -= Time.deltaTime;
        }
        else
        {
            isDashing = false;
        }
    }

    private void StartDash(float moveInput)
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        lastDashTime = Time.time;

        // Deduct stamina
        currentStamina -= dashStaminaCost;

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
            yield return new WaitForSeconds(0.1f); // Regenerate every 0.1 seconds
            Debug.Log($"Current Stamina: {currentStamina}");
            Debug.Log($"Stamina Regenerating: {currentStamina}/{maxStamina}");
        }
        isRegeneratingStamina = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
}
