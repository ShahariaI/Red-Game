using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 15f;
    private bool isFacingRight = true;
    private bool doubleJump;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private float dashStamina = 40f;
    private Animator animator;

    private float runningSpeed = 15f;
    private bool isRunning;
    private float runningCost = 20f;

    private float maxStamina = 100f;
    public float currentStamina = 0f;
    private float staminaRegenRate = 25f;

    private bool isFalling = false;
    [SerializeField] private GameObject pofe;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Staminabar staminabar;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        isRunning = Input.GetKey(KeyCode.LeftControl);
        speed = isRunning ? runningSpeed : 8f;
        animator.SetBool("isRunnig", isRunning);
        if (isRunning)
        {
            currentStamina -= runningCost * Time.deltaTime;
        }

        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump") && (IsGrounded()|| doubleJump))
        {
            animator.Play("jump start");
            print("first jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            doubleJump = !doubleJump;
            isFalling = false;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            print("double jump");
            Instantiate(pofe, transform.position, Quaternion.identity);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            
        }
        if (rb.velocity.y <= 0)
        {
            animator.SetBool("isJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && dashStamina <= currentStamina)
        {
            StartCoroutine(Dash());
        }

        Flip();

        RegenStamina();

        if(rb.velocity.y <= 0 && !IsGrounded() && !isFalling)
        {
            animator.SetBool("isFalling", true);
            animator.Play("jump mid");
            isFalling = true;
        }
        if (IsGrounded() && isFalling)
        {
            animator.Play("Landing");
            animator.SetBool("isFalling", false);
            isFalling = false;
        }
    }

    private void FixedUpdate()
    {
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
        if (isDashing)
        {
            return; 
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void RegenStamina()
    {
        if(!isDashing && IsGrounded() && currentStamina <= maxStamina && !isRunning)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
        }   
    }
    private IEnumerator Dash()
    {
     
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        if (isDashing == true)
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        if (isDashing == true)
        {
            animator.SetBool("isDashing", true);
        }
        yield return new WaitForSeconds(dashingTime);
        
        rb.gravityScale = originalGravity;
        isDashing = false;
        if (isDashing == false)
        {
            animator.SetBool("isDashing", false);
        }

        currentStamina -= dashStamina;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

       
        
    }
}
