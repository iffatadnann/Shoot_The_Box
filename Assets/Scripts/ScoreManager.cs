using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Important for UI
using UnityEngine.SceneManagement; // Needed to restart the game

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Allows cubes to find this easily

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject winUI;
    public GameObject loseUI;

    [Header("Game Settings")]
    public int scoreToWin = 100;
    public float timeRemaining = 30f; // 30 seconds to play

    private int currentScore = 0;
    private bool isGameOver = false;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        winUI.SetActive(false);
        loseUI.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return;

        // Timer Logic
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();

            // --- NEW: HURRY UP EFFECT ---
            if (timeRemaining <= 5f)
            {
                // 1. Turn text red
                timerText.color = Color.red;

                // 2. Make it pulse using a Sine wave
                // This creates a smooth 0.8 to 1.2 size loop
                float pulse = 1f + Mathf.Sin(Time.time * 10f) * 0.1f;
                timerText.transform.localScale = new Vector3(pulse, pulse, pulse);
            }
        }
        else
        {
            LoseGame();
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        scoreText.text = "Score: " + currentScore;

        if (currentScore >= scoreToWin)
        {
            WinGame();
        }
    }

    void UpdateTimerUI()
    {
        // Formats the time to look like 00:00
        timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString();
    }

    void WinGame()
    {
        if (isGameOver) return; // Prevent this from running twice
        isGameOver = true;

        // 1. Show the UI
        winUI.SetActive(true);

        // 2. Play Sound (Spatial audio makes this feel great in VR)
        //AudioSource.PlayClipAtPoint(victorySound, transform.position);

        // 3. Freeze the game world
        Time.timeScale = 0;

        // 4. (Optional) Unlock the cursor or enable the Teleport/Laser for the UI
        // If you use XR Ray Interactors, make sure they are active now so you can click "Restart"
    }
    void LoseGame()
    {
        isGameOver = true;
        loseUI.SetActive(true);
        Time.timeScale = 0;
    }

    // Call this from a UI button to play again
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
