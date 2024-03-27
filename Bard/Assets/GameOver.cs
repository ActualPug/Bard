using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] Bard bard;
    [SerializeField] GameObject gameManager;
    [SerializeField] MusicBox musicBox;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] GameObject playAgainButton;
    [SerializeField] GameObject mainMenuButton;
    bool gameEnded = false;

    // Update is called once per frame
    void Update()
    {
        if (bard.gameOver && !gameEnded) {
            EndGame();
        }
    }

    void EndGame() {
        gameEnded = true;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject enemy in enemies) {
            enemy.SetActive(false);
        }
        foreach (GameObject player in players) {
            player.SetActive(false);
        }
        musicBox.GetComponent<AudioSource>().Stop();
        DisplayEndGameUI();
    }

    void DisplayEndGameUI() {
        scoreText.enabled = false;
        gameOverText.enabled = true;
        finalScoreText.text = "Final score: " + gameManager.GetComponent<ScoreTracker>().score.ToString();
        finalScoreText.enabled = true;
        playAgainButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }
}
