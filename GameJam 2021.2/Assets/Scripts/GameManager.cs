using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject shopUI;

    GameState currentGameState = GameState.WaitingPhase;
    bool spawnersEmpty = false;

    float searchCountdown = 1f;

    private void Awake()
    {
        gameObject.name = "GameManager";
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
                //add shop mechanics here
                //stop all traps (should stop automaticly when changes Phase)
                //move player to center
                shopUI.GetComponent<ShopLogic>().DrawRandomTraps();
                shopUI.SetActive(true);
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
        spawnersEmpty = true;
    }

}
