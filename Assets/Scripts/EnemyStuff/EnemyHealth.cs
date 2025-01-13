using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50; // Health of the enemy
    public int attackDamage = 100; // Set to 1 to reduce only 1 life per attack
    public float attackRange = 1.0f; // Range of the enemy attack
    public float attackCooldown = 1.0f; // Time between attacks

    private float nextAttackTime = 0f; // Time when the enemy can attack next

    public Transform attackPoint; // Point from which the enemy attack originates
    public LayerMask playerLayer; // Layer of the player to detect

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        // Detect the player within the range of the attack
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (hitPlayer != null)
        {
            Debug.Log("Enemy attacks " + hitPlayer.name);
            PlayerHealth playerHealth = hitPlayer.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Check if the player is parrying
                if (!playerHealth.IsParrying)
                {
                    playerHealth.TakeDamage(attackDamage); // Reduce player health
                }
                else
                {
                    Debug.Log("Parried attack, no damage taken!");
                }
            }
        }
        else
        {
            Debug.Log("No player in range to attack.");
        }
    }

    // Visualize the attack range in the Unity Editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // Damage dealt to the enemy
        Debug.Log(name + " takes " + damage + " damage! Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(name + " has died!");
        Destroy(gameObject);
    }
}


