using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
    public interface IMovementBehavior
    {
        void Move(Transform enemyTransform);
    }

    public float speed = 2f;            // Speed of the enemy
    public Transform pointA;           // First point
    public Transform pointB;           // Second point
    private Vector3 targetPosition;    // Current target position
    private SpriteRenderer spriteRenderer; // For flipping sprite direction

    void Start()
    {
        // Initialize variables
        targetPosition = pointB.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move(transform);
    }

    // Implementation of the IMovementBehavior interface
    public void Move(Transform enemyTransform)
    {
        // Move the enemy towards the target position
        enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, targetPosition, speed * Time.deltaTime);

        // Check if the enemy reached the target position
        if (Vector3.Distance(enemyTransform.position, targetPosition) < 0.1f)
        {
            // Switch target position
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;

            // Flip the sprite direction
            spriteRenderer.flipX = targetPosition == pointA.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawSphere(pointB.transform.position, 0.5f);
    }



}


