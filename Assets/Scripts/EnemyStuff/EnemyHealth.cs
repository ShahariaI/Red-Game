using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50;
    public int attackDamage = 10; // Damage dealt by the enemy
    public float attackRange = 1.0f; // Range of the enemy attack
    public float attackCooldown = 1.0f; // Time between attacks

    private float nextAttackTime = 0f; // Time when the enemy can attack next

    public Transform attackPoint; // Point from which the enemy attack originates
    public LayerMask playerLayer; // Layer of the player to detect

    public void TakeDamage(int damage)
    {
        health -= damage;
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
        // Detect player within range of the attack
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        // Damage the detected player
        foreach (Collider2D player in hitPlayers)
        {
            Debug.Log("Enemy attacks " + player.name);
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
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
}
