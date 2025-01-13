using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is the player
        if (collision.CompareTag("Player"))
        {
            // Call the PlayerDied method from GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RespawnPlayer();
            }
            else
            {
                Debug.LogError("GameManager instance not found!");
            }
        }
    }
}
