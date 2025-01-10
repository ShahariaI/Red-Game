using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyWalker;

public class EnemyController : MonoBehaviour
{
    private IMovementBehavior movementBehavior;

    void Start()
    {
        // Assign a default movement behavior
        movementBehavior = GetComponent<IMovementBehavior>();
    }

    void Update()
    {
        // Call the movement behavior
        movementBehavior?.Move(transform);
    }

    public void SetMovementBehavior(IMovementBehavior newBehavior)
    {
        movementBehavior = newBehavior;
    }
}

