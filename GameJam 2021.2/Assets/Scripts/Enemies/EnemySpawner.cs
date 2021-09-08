using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] Wave[] waves;
    [Space(10)]
    [SerializeField] Transform[] spawnPoints;
    [Space(10)]
    [SerializeField] float timeBetweenWaves = 2f;

    bool isSpawning = false;
    private int nextWaveIndex = 0;
    private float waveCountdown;
    GameManager gameManager;
    bool spawnerEmpty = false;

    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public int amount;
        public float spawnRate;
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (waveCountdown <= 0)
        {
            if (!isSpawning && !spawnerEmpty)
            {
                StartCoroutine(SpawnWave(waves[nextWaveIndex]));
            }
        }
        else if (gameManager.GetCurrentState() == GameState.CombatPhase)
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        isSpawning = true;

        for (int i = 0; i < wave.amount; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        WaveCompleted();
        yield break;
    }

    void WaveCompleted()
    {
        isSpawning = false;
        waveCountdown = timeBetweenWaves;

        if (nextWaveIndex + 1 > waves.Length - 1)
        {
            spawnerEmpty = true;
            gameManager.spawnerDrained();
            Destroy(gameObject);
        }
        else
        {
            nextWaveIndex++;
        }

    }

    void SpawnEnemy(GameObject enemy)
    {
        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemySpawned = Instantiate(enemy, randomSpawn.position, randomSpawn.rotation);
    }

}
