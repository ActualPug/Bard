using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] ScreenFader screenFader;
    [SerializeField] int currentLevel = 1;
    [SerializeField] MusicBoxSO musicBoxSO;
    [SerializeField] AudioSource music;

    public void PlayAgain() {
        screenFader.FadeToColor("Level1");
        musicBoxSO.playbackPosition = music.time;
    }

    public void NextLevel() {
        int nextLevel = currentLevel + 1;
        screenFader.FadeToColor("Level" + nextLevel.ToString());
        musicBoxSO.playbackPosition = music.time;
    }

    public void MainMenu() {
        screenFader.FadeToColor("MainMenu");
        musicBoxSO.playbackPosition = music.time;
    }

    public void Play() {
        screenFader.FadeToColor("Level1");
        musicBoxSO.playbackPosition = music.time;
    }

    public void Quit() {
        Application.Quit();
    }
}
