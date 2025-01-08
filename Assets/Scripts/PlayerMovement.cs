using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public interface IMovable
    {
        void Move(Vector2 direction);
        void Jump(float jumpForce);
    }

    public interface IInteractable
    {
        void Interact();
    }

    public interface ICameraFollow
    {
        void Follow(Transform target, Vector3 offset);
    }

    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        Move(new Vector2(moveInput, 0));

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump(jumpForce);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
