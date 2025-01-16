using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyWalker;


public class EnemyHover : MonoBehaviour, IMovementBehavior
{
    public float hoverSpeed = 1f;  // Speed of the horizontal movement
    public float hoverRange = 1f; // Range of the horizontal movement
    private float initialX;       // Initial x position

    void Start()
    {
        // Store the initial x position
        initialX = transform.position.x;
    }

    public void Move(Transform enemyTransform)
    {
        // Calculate the new x position based on a sine wave
        float newX = initialX + Mathf.Sin(Time.time * hoverSpeed) * hoverRange;

        // Update the enemy's position, keeping y and z the same
        enemyTransform.position = new Vector3(newX, enemyTransform.position.y, enemyTransform.position.z);
    }
}


