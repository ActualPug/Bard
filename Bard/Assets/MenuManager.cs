using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] ScreenFader screenFader;
    [SerializeField] int currentLevel = 1;

    public void PlayAgain() {
        screenFader.FadeToColor("Level1");
    }

    public void NextLevel() {
        int nextLevel = currentLevel + 1;
        screenFader.FadeToColor("Level" + nextLevel.ToString());
    }

    public void MainMenu() {
        screenFader.FadeToColor("MainMenu");
    }
}
