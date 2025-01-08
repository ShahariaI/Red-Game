using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void LateUpdate()
    {
        Follow(player, offset);
    }

    public void Follow(Transform target, Vector3 offset)
    {
        transform.position = target.position + offset;
    }
}
