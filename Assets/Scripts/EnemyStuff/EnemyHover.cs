using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyWalker;


public class EnemyHover : MonoBehaviour
{
    public float hoverSpeed = 2f;  // Speed of the hovering movement
    public float hoverRange = 2f; // Distance the enemy moves back and forth

    private Vector2 startPosition; // Starting position of the enemy
    private float hoverTime;       // Tracks time for movement

    void Start()
    {
        // Record the starting position
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the side-to-side movement
        hoverTime += Time.deltaTime * hoverSpeed;
        float offset = Mathf.Sin(hoverTime) * hoverRange;
        transform.position = new Vector2(startPosition.x + offset, startPosition.y);
    }
}


