using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scuicidebomb : MonoBehaviour
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

    Animator animator;

    public EnemyAI enemyAI;
    public PlayerHealth playerHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (enemyAI && cooldownTimer >= attackCooldown)
        {
           
            cooldownTimer = 0; // Reset cooldown timer
        }
    }

  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(damage);
            animator.SetTrigger("isDead");

            boxCollider.enabled = false;
            enemyAI.enabled = false;


            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator HandleDeath()
    {
        // Wait for the length of the death animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        boxCollider.enabled = false;
        circleCollider.enabled = false;
        enemyAI.enabled = false;

       
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
   
}
