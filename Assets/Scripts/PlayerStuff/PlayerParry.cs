using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ParryScript : MonoBehaviour
{
    public float parryCooldown = 1f; // Time between parry actions
    private bool canParry = true; // To track whether parry is allowed

    public Animator animator; // Animator to play the parry animation
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>(); // Access the PlayerHealth component
    }

    private void Update()
    {
        // Player presses 'P' to attempt a parry (or you can customize this to other keys)
        if (Input.GetKeyDown(KeyCode.P) && canParry)
        {
            ParryAttack();
        }
    }

    private void ParryAttack()
    {
        // Set the player to be in a parrying state
        playerHealth.SetParrying(true);

        // Play parry animation (optional)
       
        // Disable the ability to parry for a short time
        canParry = false;

        // Start cooldown before the next parry is allowed
        StartCoroutine(ParryCooldown());

        // Reset parry state after a short delay
        StartCoroutine(ResetParryState());
    }

    private IEnumerator ParryCooldown()
    {
        // Wait for the cooldown duration
        yield return new WaitForSeconds(parryCooldown);
        canParry = true;
    }

    private IEnumerator ResetParryState()
    {
        // Reset the parry state after a short delay (to match animation timing or the parry window)
        yield return new WaitForSeconds(0.3f); // Adjust timing based on your animation and needs
        playerHealth.SetParrying(false);
    }
}

