using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTracker : MonoBehaviour
{
    [SerializeField] Bard bard;
    [SerializeField] GameObject gameManager;
    [SerializeField] MusicBox musicBox;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI scoreToWinText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI youWinText;
    [SerializeField] GameObject playAgainButton;
    [SerializeField] GameObject mainMenuButton;
    [SerializeField] GameObject nextLevelButton;
    bool gameEnded = false;

    void Start() {
        gameEnded = false;
        scoreText.text = gameManager.GetComponent<ScoreTracker>().score.ToString();
        scoreToWinText.text = "Score needed: " + gameManager.GetComponent<ScoreTracker>().scoreToWin.ToString();
        Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        // check for game over
        if (bard.gameOver && !gameEnded) {
            EndGame();
        }

        // check for game win
        if (gameManager.GetComponent<ScoreTracker>().score >= gameManager.GetComponent<ScoreTracker>().scoreToWin && !gameEnded) {
            WinGame();
        }
    }

    void EndGame() {
        gameEnded = true;
        DisableEntities();
        musicBox.GetComponent<AudioSource>().Stop();
        Time.timeScale = 0;
        DisplayEndGameUI();
        Debug.Log("Game lost!");
    }

    void WinGame() {
        gameEnded = true;
        DisableEntities();
        musicBox.GetComponent<AudioSource>().Stop();
        Time.timeScale = 0;
        DisplayWinGameUI();
        Debug.Log("Game win!");
    }

    void DisplayEndGameUI() {
        scoreText.enabled = false;
        gameOverText.enabled = true;
        finalScoreText.text = "Final score: " + gameManager.GetComponent<ScoreTracker>().score.ToString();
        finalScoreText.enabled = true;
        playAgainButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    void DisplayWinGameUI() {
        youWinText.enabled = true;
        finalScoreText.text = "Now continue on to the next level!";
        finalScoreText.enabled = true;
        nextLevelButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    void DisableEntities() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject enemy in enemies) {
            enemy.SetActive(false);
        }
        foreach (GameObject player in players) {
            player.SetActive(false);
        }
    }
}
