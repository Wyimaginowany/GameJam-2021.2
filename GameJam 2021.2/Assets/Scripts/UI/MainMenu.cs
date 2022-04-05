using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider buttonVolumeSlider;
    [SerializeField] Slider playerVolumeSlider;
    [SerializeField] Slider enemyVolumeSlider;
    [SerializeField] Slider trapsVolumeSlider;

    float masterVolume;
    float musicVolume;
    float buttonVolume;
    float playerVolume;
    float enemyVolume;
    float trapsVolume;

    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        if (masterVolumeSlider == null) return;

        audioMixer.GetFloat("master", out masterVolume);
        masterVolumeSlider.value = masterVolume;

        audioMixer.GetFloat("music", out musicVolume);
        musicVolumeSlider.value = musicVolume;

        audioMixer.GetFloat("enemy", out enemyVolume);
        enemyVolumeSlider.value = enemyVolume;

        audioMixer.GetFloat("player", out playerVolume);
        playerVolumeSlider.value = playerVolume;

        audioMixer.GetFloat("buttons", out buttonVolume);
        buttonVolumeSlider.value = buttonVolume;

        audioMixer.GetFloat("turrets", out trapsVolume);
        trapsVolumeSlider.value = trapsVolume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("master", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("music", volume);
    }

    public void SetButtonSoundsVolume(float volume)
    {
        audioMixer.SetFloat("buttons", volume);
    }

    public void SetPlayerVolume(float volume)
    {
        audioMixer.SetFloat("player", volume);
    }

    public void SetEnemyVolume(float volume)
    {
        audioMixer.SetFloat("enemy", volume);
    }

    public void SetTrapVolume(float volume)
    {
        audioMixer.SetFloat("turrets", volume);
    }

    public void ShowCredits()
    {
        //credits UI set active true
    }

    public void LoadLevel(int levelIndex)
    {
        //load scene (levelIndex)
    }


}
