using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] ScreenFader screenFader;

    public void PlayAgain() {
        screenFader.FadeToColor("Level1");
    }

    public void MainMenu() {
        screenFader.FadeToColor("MainMenu");
    }
}
