using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool IsParrying { get; private set; } // Whether the player is currently parrying

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance; // Access the GameManager to get player lives
    }

    public void TakeDamage(int damage)
    {
        if (IsParrying)
        {
            // If the player is parrying, don't take damage
            Debug.Log("Parry successful! No damage taken.");
            return;
        }

        // Reduce lives by 1 when damage is taken (not health)
        gameManager.playerLives -= damage;

        Debug.Log("Player takes damage! Remaining lives: " + gameManager.playerLives);

        if (gameManager.playerLives <= 0)
        {
            // Trigger respawn logic when lives are 0
            gameManager.RespawnPlayer();
        }
    }

    public void SetParrying(bool isParrying)
    {
        IsParrying = isParrying;
    }
}




