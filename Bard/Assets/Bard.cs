using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bard : MonoBehaviour
{
    [SerializeField] Creature bardCreature;
    bool gamePaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bardCreature.health <= 0 && !gamePaused) {
            Death();
        }
    }

    void Death() {
        PauseGame();
    }

    void PauseGame() {
        Time.timeScale = 0;
        gamePaused = true;
    }
}
