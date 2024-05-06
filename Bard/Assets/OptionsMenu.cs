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

    [SerializeField] Canvas optionsCanvas;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("MasterVolume") == 0) { // first time playing
            masterVolumeSlider.value = 1;
        }
        else { // load preference
            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        }
        if (PlayerPrefs.GetFloat("MusicVolume") == 0) { // first time playing
            musicVolumeSlider.value = 1;
        }
        else { // load preference
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        if (PlayerPrefs.GetFloat("SFXVolume") == 0) { // first time playing
            sfxVolumeSlider.value = 1;
        }
        else { // load preference
            musicVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        GetResolutionOptions();
        resDropdown.value = PlayerPrefs.GetInt("ResolutionIndex");
        if (PlayerPrefs.GetInt("FullscreenToggled") == 1 || PlayerPrefs.GetInt("FullscreenToggled") == 0) {
            fullscreenToggle.isOn = true;
        }
        else {
            fullscreenToggle.isOn = false;
        }
        if (PlayerPrefs.GetInt("VSyncEnabled") == 1 || PlayerPrefs.GetInt("VSyncEnabled") == 0) {
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

    void GetResolutionOptions() {
        resDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        resDropdown.options.Add(new TMP_Dropdown.OptionData("1920x1080"));
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
            PlayerPrefs.SetInt("FullscreenToggled", 1);
        }
        else {
            PlayerPrefs.SetInt("FullscreenToggled", 2);
        }
    }

    public void SetVSync() {
        QualitySettings.vSyncCount = vsyncToggle.isOn ? 1 : 0;
        if (vsyncToggle.isOn) {
            PlayerPrefs.SetInt("VSyncEnabled", 1);
        }
        else {
            PlayerPrefs.SetInt("VSyncEnabled", 2);
        }
    }

    public void OpenOptions() {
        optionsCanvas.enabled = true;
    }

    public void CloseOptions() {
        optionsCanvas.enabled = false;
        Time.timeScale = 1;
    }

    public void ResetPreferences() {
        PlayerPrefs.SetFloat("MasterVolume", 0);
        PlayerPrefs.SetFloat("MusicVolume", 0);
        PlayerPrefs.SetFloat("SFXVolume", 0);
        PlayerPrefs.SetInt("ResolutionIndex", 0);
        PlayerPrefs.SetInt("FullscreenToggled", 0);
        PlayerPrefs.SetInt("VSyncEnabled", 0);
        masterVolumeSlider.value = 1;
        musicVolumeSlider.value = 1;
        sfxVolumeSlider.value = 1;
        resDropdown.value = 0;
        fullscreenToggle.isOn = true;
        vsyncToggle.isOn = true;
    }
}
