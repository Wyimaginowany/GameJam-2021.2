using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject[] thingsToHide;
    [SerializeField] LevelLoader loader;

    public void RefreshPauseMenu()
    {
        foreach (GameObject thing in thingsToHide)
        {
            thing.SetActive(false);
        }
        pauseMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        loader.LoadClickedLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        loader.LoadClickedLevel(0);
    }

}
