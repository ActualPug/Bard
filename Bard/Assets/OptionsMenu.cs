using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;

    [Header("Resolution")]
    [SerializeField] TMP_Dropdown resDropdown;
    Resolution[] resolutions;
    [SerializeField] Toggle fullscreenToggle;

    [Header("Postprocessing")]
    [SerializeField] Toggle vsyncToggle;

    [Header("Others")]
    [SerializeField] Canvas optionsCanvas;
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource sfxAudioSource;

    /*void Awake() {
        // first time playing game set default preferences
        if (!PlayerPrefs.HasKey("MasterVolume")) {
            PlayerPrefs.SetFloat("MasterVolume", 1);
        }
        if (!PlayerPrefs.HasKey("MusicVolume")) {
            PlayerPrefs.SetFloat("MusicVolume", 1);
        }
        if (!PlayerPrefs.HasKey("SFXVolume")) {
            PlayerPrefs.SetFloat("SFXVolume", 1);
        }
        if (!PlayerPrefs.HasKey("ResolutionIndex")) {
            PlayerPrefs.SetFloat("ResolutionIndex", Screen.resolutions.Length - 1);
        }
        if (!PlayerPrefs.HasKey("FullscreenEnabled")) {
            PlayerPrefs.SetFloat("FullscreenEnabled", 1);
        }
        if (!PlayerPrefs.HasKey("VSyncEnabled")) {
            PlayerPrefs.SetFloat("VSyncEnabled", 1);
        }
    }*/
    // Start is called before the first frame update
    void Start()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");     
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        GetResolutionOptions();
        resDropdown.value = PlayerPrefs.GetInt("ResolutionIndex");
        if (PlayerPrefs.GetInt("FullscreenEnabled") == 1) {
            fullscreenToggle.isOn = true;
        }
        else {
            fullscreenToggle.isOn = false;
        }
        if (PlayerPrefs.GetInt("VSyncEnabled") == 1) {
            vsyncToggle.isOn = true;
        }
        else {
            vsyncToggle.isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVolume() {
        audioMixer.SetFloat("MasterVolume", ConvertToDb(masterVolumeSlider.value));
        PlayerPrefs.SetFloat("MasterVolume", Mathf.Max(masterVolumeSlider.value, 0.001f));
    }

    public void SetMusicVolume() {
        audioMixer.SetFloat("MusicVolume", ConvertToDb(musicVolumeSlider.value));
        PlayerPrefs.SetFloat("MusicVolume", Mathf.Max(musicVolumeSlider.value, 0.001f));
    }

    public void SetSFXVolume() {
        audioMixer.SetFloat("SFXVolume", ConvertToDb(sfxVolumeSlider.value));
        PlayerPrefs.SetFloat("SFXVolume", Mathf.Max(sfxVolumeSlider.value, 0.001f));
    }

    float ConvertToDb(float sliderValue) {
        return Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20;
    }

    public void PlayMusic() {
        if (!musicAudioSource.isPlaying) {
            musicAudioSource.Play();
        }
    }

    public void PlaySFX() {
        if (!sfxAudioSource.isPlaying) {
            sfxAudioSource.Play();
        }
    }

    public void StopMusic() {
        musicAudioSource.Stop();
    }

    public void StopSFX() {
        sfxAudioSource.Stop();
    }

    void GetResolutionOptions() {
        resDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++) {
            TMP_Dropdown.OptionData newOption;
            newOption = new TMP_Dropdown.OptionData(resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString());
            resDropdown.options.Add(newOption);
        }
    }

    public void ChooseResolution() {
        Screen.SetResolution(resolutions[resDropdown.value].width, resolutions[resDropdown.value].height, fullscreenToggle.isOn);
        PlayerPrefs.SetInt("ResolutionIndex", resDropdown.value);
    }

    public void SetFullscreen() {
        Screen.SetResolution(resolutions[resDropdown.value].width, resolutions[resDropdown.value].height, fullscreenToggle.isOn);
        if (fullscreenToggle.isOn) {
            PlayerPrefs.SetInt("FullscreenEnabled", 1);
        }
        else {
            PlayerPrefs.SetInt("FullscreenEnabled", 0);
        }
    }

    public void SetVSync() {
        QualitySettings.vSyncCount = vsyncToggle.isOn ? 1 : 0;
        if (vsyncToggle.isOn) {
            PlayerPrefs.SetInt("VSyncEnabled", 1);
        }
        else {
            PlayerPrefs.SetInt("VSyncEnabled", 0);
        }
    }

    public void OpenOptions() {
        optionsCanvas.enabled = true;
    }

    public void CloseOptions() {
        optionsCanvas.enabled = false;
        Time.timeScale = 1;
        music.Play();
    }

    public void ResetPreferences() {
        PlayerPrefs.SetFloat("MasterVolume", 1);
        PlayerPrefs.SetFloat("MusicVolume", 1);
        PlayerPrefs.SetFloat("SFXVolume", 1);
        PlayerPrefs.SetInt("ResolutionIndex", Screen.resolutions.Length - 1);
        PlayerPrefs.SetInt("FullscreenToggled", 1);
        PlayerPrefs.SetInt("VSyncEnabled", 1);
        masterVolumeSlider.value = 1;
        musicVolumeSlider.value = 1;
        sfxVolumeSlider.value = 1;
        resDropdown.value = Screen.resolutions.Length - 1;
        fullscreenToggle.isOn = true;
        vsyncToggle.isOn = true;
    }
}
