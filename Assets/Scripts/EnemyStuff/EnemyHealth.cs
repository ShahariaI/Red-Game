using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 50; // Health of the enemy
    public int damage = 1;
    public float attackRange = 1.0f; // Range of the enemy attack
    public float attackCooldown = 1.0f; // Time between attacks

    private float nextAttackTime = 1f; // Time when the enemy can attack next

    public Transform attackPoint; // Point from which the enemy attack originates
    public LayerMask playerLayer; // Layer of the player to detect

    public PlayerHealth playerHealth;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public void Attack()
    {
        // Detect player within range of the attack
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        print(hitPlayers.Length);

        // Damage the detected player
        foreach (Collider2D player in hitPlayers)
        {
            Debug.Log("Enemy attacks " + player.name);
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
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


