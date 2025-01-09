using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton pattern for global access

    [Header("Game Settings")]
    public int playerLives = 3;

    [Header("UI References")]
    public TextMeshProUGUI livesText; // Using TextMeshProUGUI
    public GameObject gameOverUI;

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

    private void UpdateUI()
    {
        if (livesText != null)
            livesText.text = "Lives: " + playerLives;
    }
}
