using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("To Attach")]
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject player;

    int currentStage = 0;
    int spawnersLeft;
    GameState currentGameState = GameState.WaitingPhase;
    bool spawnersEmpty = false;
    float searchCountdown = 1f;


    [System.Serializable]
    public class GameStages
    {
        public GameObject[] enemySpawners;
    }
    public GameStages[] gameStages;

    private void Awake()
    {
        gameObject.name = "GameManager";
        spawnersLeft = gameStages[0].enemySpawners.Length;
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
                SetState(GameState.BuildPhase);
                //move player to center
                shopUI.GetComponent<ShopLogic>().DrawRandomShop();
                shopUI.SetActive(true);
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
}
