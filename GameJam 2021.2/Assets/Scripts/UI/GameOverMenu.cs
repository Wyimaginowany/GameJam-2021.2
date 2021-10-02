using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] GameObject playerUI;
    [SerializeField] LevelLoader loader;


    public void Exit()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        gameObject.SetActive(false);
        loader.LoadClickedLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        gameObject.SetActive(false);
        playerUI.SetActive(false);
        loader.LoadClickedLevel(0);
    }
}
