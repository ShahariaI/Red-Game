using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyWalker;

public class EnemyHover : MonoBehaviour, IMovementBehavior
{
    public float hoverSpeed = 1f;
    public float hoverRange = 1f;
    private float initialX;

    void Start()
    {
        initialX = transform.position.x;
    }

    public void Move(Transform enemyTransform)
    {
        // Hover up and down
        float newY = initialX + Mathf.Sin(Time.time * hoverSpeed) * hoverRange;
        enemyTransform.position = new Vector3(enemyTransform.position.x, newY, enemyTransform.position.z);
    }
}

