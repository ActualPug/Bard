using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTracker : MonoBehaviour
{
    [SerializeField] Bard bard;
    [SerializeField] GameObject gameManager;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI scoreToWinText;
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] Canvas youWinCanvas;
    [SerializeField] TextMeshProUGUI finalScoreText;
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
            gameOverCanvas.enabled = true;
            finalScoreText.text = "Final score: " + gameManager.GetComponent<ScoreTracker>().score.ToString();
        }

        // check for game win
        if (gameManager.GetComponent<ScoreTracker>().score >= gameManager.GetComponent<ScoreTracker>().scoreToWin && !gameEnded) {
            EndGame();
            youWinCanvas.enabled = true;
        }
    }

    void EndGame() {
        gameEnded = true;
        DisableEntities();
        musicAudioSource.Stop();
        Time.timeScale = 0;
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
