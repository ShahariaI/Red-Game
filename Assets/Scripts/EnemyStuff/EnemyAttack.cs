using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] public int damage;
    [SerializeField] private LayerMask playerLayer; // Renamed for clarity
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private CircleCollider2D circleCollider;

    private float cooldownTimer = Mathf.Infinity;
    public Transform attackPoint;
    public int health = 50;
    public float attackRange = 1.0f;

    public EnemyAI enemyAI;
    public PlayerHealth playerHealth;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (enemyAI && cooldownTimer >= attackCooldown)
        {
            PerformAttack();
            cooldownTimer = 0; // Reset cooldown timer
        }
    }

    private void PerformAttack()
    {
        // Perform a raycast in the direction of the attack point
        RaycastHit2D hit = Physics2D.CircleCast(
            attackPoint.position,
            attackRange,
            Vector2.zero,
            0,
            playerLayer
        );

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            // Damage the player
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
