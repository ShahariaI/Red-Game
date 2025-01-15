using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] public int damage;
    [SerializeField] private LayerMask player;


    private float cooldownTimer = Mathf.Infinity;
    public Transform attackPoint;
    public int health = 50;
    public float attackRange = 1.0f;

    public EnemyAI enemyAI;
    public PlayerHealth playerHealth;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (enemyAI)
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;

            }

        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }

    



    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }



}
