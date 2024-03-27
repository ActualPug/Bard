using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] Bard bard;
    public int score = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        StartTracking();
    }

    void StartTracking() {
        StartCoroutine(StartTrackingRoutine());
    }

    IEnumerator StartTrackingRoutine() {
        while(!bard.gameOver) {
            yield return new WaitForSeconds(0.1f);
            score += 10;
            scoreText.text = score.ToString();
        }
    }
}
