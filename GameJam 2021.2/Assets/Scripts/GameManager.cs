using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    float searchCountdown = 1f;

    [Header("To Attach")]
    [SerializeField] GameObject shopUI;

    GameState currentGameState = GameState.WaitingPhase;
    int currentStageIndex = 0;
    bool spawnersEmpty = false;

    [System.Serializable]
    public class GameStages
    {
        public GameObject[] enemySpawners;
    }
    public GameStages[] gameStages;

    private void Awake()
    {
        gameObject.name = "GameManager";
    }

    private void Update()
    {
        if (spawnersEmpty)
        {
            CheckIfEnemyAlive();
        }
    }

    void CheckIfEnemyAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0 && currentGameState != GameState.BuildPhase)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                SetState(GameState.BuildPhase);
                //move player to center
                shopUI.GetComponent<ShopLogic>().DrawRandomTraps();
                shopUI.SetActive(true);
                spawnersEmpty = false;
            }
        }
        return;
    }

    public void SetState(GameState newGameState)
    {
        currentGameState = newGameState;
    }

    public GameState GetCurrentState()
    {
        return currentGameState;
    }

    public void spawnerDrained()
    {
        if (gameStages[currentStageIndex].enemySpawners.Length == 1)
        {
        spawnersEmpty = true;
        }
    }

}
