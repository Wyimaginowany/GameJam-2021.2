using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int gridSize = 3;
    public float trapMinX = -45f;
    public float trapMaxX = 45f;
    public float trapMinY = -24f;
    public float trapMaxY = 24f;


    [Header("To Attach")]
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerUI;
    [SerializeField] GameObject winScreen;

    bool gameOver = false;
    int currentStage = 0;
    int spawnersLeft;
    GameState currentGameState = GameState.WaitingPhase;
    bool spawnersEmpty = false;
    float searchCountdown = 1f;


    [System.Serializable]
    public class GameStages
    {
        public GameObject[] enemySpawners;
        public int moneyReward;
    }
    public GameStages[] gameStages;

    private void Awake()
    {
        gameObject.name = "GameManager";
        spawnersLeft = gameStages[0].enemySpawners.Length;
        bool gameOver = false;
    }

    private void Update()
    {
        if (spawnersEmpty)
        {
            EnemyIsAlive();
        }
    }

    void EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0 && currentGameState != GameState.BuildPhase)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                shopUI.GetComponent<ShopLogic>().DrawRandomShop();
                Invoke("EndPhase", 1f);
                spawnersEmpty = false;
            }
        }
        return;
    }
    
    public void spawnerDrained()
    {
        spawnersLeft--;
        if (spawnersLeft <= 0)
        {
            spawnersEmpty = true;
        }
    }

    public void StartNextStage()
    {
        player.GetComponent<PlayerHealth>().invincible = false;
        currentStage++;
        if (currentStage <= gameStages.Length - 1)
        {
            spawnersLeft = gameStages[currentStage].enemySpawners.Length;
            foreach (GameObject spawner in gameStages[currentStage].enemySpawners)
            {
                spawner.SetActive(true);
            }
        }
    }

    public void EndPhase()
    {
        if (currentStage == gameStages.Length - 1)
        {
            GameOver();
            Time.timeScale = 0f;
            winScreen.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            return;
        }

        player.GetComponent<PlayerHealth>().invincible = true;
        SetState(GameState.BuildPhase);
        //move player to center
        playerUI.SetActive(false);
        shopUI.SetActive(true);
        shopUI.GetComponent<ShopLogic>().GiveMoneyToPlayer(gameStages[currentStage].moneyReward);
        shopUI.GetComponent<ShopLogic>().RefreshStats();
    }

    #region For other scripts

    public void SetState(GameState newGameState)
    {
        currentGameState = newGameState;
    }

    public GameState GetCurrentState()
    {
        return currentGameState;
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    #endregion

    public bool isOver()
    {
        return gameOver;
    }

    public void GameOver()
    {
        gameOver = true;
    }
}
