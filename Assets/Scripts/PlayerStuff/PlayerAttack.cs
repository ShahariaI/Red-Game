using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;

    // Attack parameters
    public float attackRange = 1.0f; // Range of the attack
    public int attackDamage = 10; // Damage dealt by the attack
    public float attackCooldown = 0.5f; // Time between attacks

    // Reference to the attack point
    public Transform attackPoint; // Point from which the attack originates
    public LayerMask enemyLayer; // Layer of enemies to detect

    private float nextAttackTime = 0f; // Time when the player can attack next

    public List<string> Attacks;
    private int i = 0;
    
    private void Start()
    {
        Attacks.Add("atak");
        Attacks.Add("attak 2");
        Attacks.Add("attack 3");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
           
        }
    }

    void Attack()
    {

        animator.Play(Attacks[i]);
        i++;

        if(i >= Attacks.Count)
        {
            i = 0;
        }
        // Play attack animation (if applicable)
        Debug.Log("Player attacks!");

        // Detect enemies within range of the attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        

        // Damage the detected enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
            EnemyHealth enemyScript = enemy.GetComponent<EnemyHealth>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(attackDamage);
            }
        }
    }

    // Visualize the attack range in the Unity Editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}