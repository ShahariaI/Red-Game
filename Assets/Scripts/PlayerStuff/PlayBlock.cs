using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    [SerializeField] private float blockDuration = 2.0f; // Maximum time the player can block
    [SerializeField] private float blockCooldown = 1.0f; // Cooldown before the player can block again
    private bool isBlocking = false; // Whether the player is currently blocking
    private bool canBlock = true;   // Whether the player is allowed to block

    private PlayerHealth playerHealth;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        // Start blocking when the player presses the block key (e.g., LeftShift)
        if (Input.GetKeyDown(KeyCode.C) && canBlock)
        {
            StartCoroutine(Block());
        }
    }

    private IEnumerator Block()
    {
        animator.Play("parry");

        isBlocking = true;
        canBlock = false;
        playerHealth.SetBlocking(true); // Notify PlayerHealth to negate damage
        Debug.Log("Player is blocking!");

        // Keep blocking for the specified duration
        yield return new WaitForSeconds(blockDuration);

        isBlocking = false;
        playerHealth.SetBlocking(false); // Notify PlayerHealth to stop blocking
        Debug.Log("Player stopped blocking!");

        // Wait for the cooldown before allowing blocking again
        yield return new WaitForSeconds(blockCooldown);

        canBlock = true;
    }
}
