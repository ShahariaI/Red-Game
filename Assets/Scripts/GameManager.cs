using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton pattern for global access

    [Header("Game Settings")]
    public int playerLives = 3; // Set starting lives to 3

    [Header("Player References")]
    public GameObject player; // Reference to the player GameObject
    public Transform spawnPoint; // The spawn point for respawn (set this in the Inspector)

    [Header("Player References")]
    public TextMeshProUGUI livesText;


    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

<<<<<<< Updated upstream
    private void Start()
    {
        UpdateUI();
    }

    public void PlayerDied()
    {
        playerLives--;
        UpdateUI();

        if (playerLives <= 0)
        {
            RestartLevel();
        }

    }

    private void RestartLevel()
    {
        // Restart the current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GameOver()
    {
        // Show Game Over UI
        gameOverUI.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    public void RestartGame()
    {
        // Reset game state
        playerLives = 3;
        UpdateUI();

        Time.timeScale = 1; // Resume the game
        SceneManager.LoadScene(0); // Load the first scene (assuming it's the main menu)
    }
=======
  
>>>>>>> Stashed changes

    private void UpdateUI()
    {
        if (livesText != null)
            livesText.text = "Lives: " + playerLives;
    }

    public void RespawnPlayer()
    {
        if (playerLives > 0)
        {
            // Reset lives and update the UI
            playerLives = 3;
            

            // Respawn the player at the spawn point
            player.transform.position = spawnPoint.position;

            // Enable the player (if previously disabled)
            player.SetActive(true);
        }
        else
        {
            // If no lives left, you can add custom logic here (e.g., restart level)
            // But for now, it respawns with 3 lives.
            playerLives = 3;
            
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        // Restart the current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}

