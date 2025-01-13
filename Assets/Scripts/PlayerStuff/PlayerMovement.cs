using UnityEngine;
using TMPro; // Ensure you include this for TextMeshProUGUI
using System.Collections;

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

    // TextMeshPro reference
    public TextMeshProUGUI staminaText; // Reference to TextMeshPro UI component

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDashing;
    private float dashTimeLeft;
    private float lastDashTime;

    private SpriteRenderer spriteRenderer; // SpriteRenderer reference

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Initialize the SpriteRenderer
        currentStamina = maxStamina;

        // Initialize the stamina text
        UpdateStaminaText();
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

        // Flip the sprite based on movement direction
        if (moveInput > 0)
        {
            spriteRenderer.flipX = false; // Face right
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = true; // Face left
        }

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
        UpdateStaminaText(); // Update stamina text
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
        if (collision.gameObject.CompareTag("Enemy")) // Replace 'Enemy' with the appropriate tag
        {
            GameManager.Instance.PlayerDied(); // Notify GameManager of the player's death
        }



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