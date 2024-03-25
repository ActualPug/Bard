using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] MusicBox musicBox;
    int score = 0;
    bool finalScoreUpdated = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        StartTracking();
    }

    void Update() {
        if (Time.timeScale == 0 && !finalScoreUpdated) {
            Debug.Log("Time scale is 0");
            scoreText.enabled = false;
            gameOverText.enabled = true;
            finalScoreText.text = "Final score: " + score.ToString();
            finalScoreText.enabled = true;
            finalScoreUpdated = true;
            musicBox.GetComponent<AudioSource>().Stop();
        }
    }

    void StartTracking() {
        StartCoroutine(StartTrackingRoutine());
    }

    IEnumerator StartTrackingRoutine() {
        while(true) {
            yield return new WaitForSeconds(0.1f);
            score += 10;
            scoreText.text = score.ToString();
        }
    }
}
