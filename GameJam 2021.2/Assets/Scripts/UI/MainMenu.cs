using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Debug.Log(isFullscreen);
        Screen.fullScreen = isFullscreen;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("music", volume);
    }

    public void SetSoundsVolume(float volume)
    {
        audioMixer.SetFloat("sounds", volume);
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
