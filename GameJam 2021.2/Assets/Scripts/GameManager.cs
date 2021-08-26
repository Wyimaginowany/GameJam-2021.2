using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameState currentGameState = GameState.WaitingPhase;
    [SerializeField] bool spawnersEmpty = false;

    private float searchCountdown = 1f;

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
        if (searchCountdown <= 0)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                SetState(GameState.BuildPhase);
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
