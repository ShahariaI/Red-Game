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
           
        }
        else
        {
            RestartLevel();
        }

    }

    private void RestartLevel()
    {
        // Restart the current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

