using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }

    private bool isBlocking = false; // Tracks if the player is blocking

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        if (isBlocking)
        {
            Debug.Log("Damage negated due to blocking!");
            return; // Completely negate the damage
        }

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth <= 0)
        {
            // Player death logic
            Debug.Log("Player is dead!");
            Destroy(gameObject);
        }
    }

    public void SetBlocking(bool blocking)
    {
        isBlocking = blocking;
    }
}
