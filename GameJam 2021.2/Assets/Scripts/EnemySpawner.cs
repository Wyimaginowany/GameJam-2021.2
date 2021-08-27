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

    private bool spawning = false;
    private enum SpawnState {SPAWNING, COUNTING};
    private int nextWaveIndex = 0;
    private float waveCountdown;
    private SpawnState spawnState = SpawnState.COUNTING;
    GameManager gameManager;
    bool spawnerEmpty = false;
    bool isSpawning = false;

    [System.Serializable]
    public class Wave
    {
        public Transform enemyPrefab;
        public int amount;
        public float spawnRate;
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        waveCountdown = timeBetweenWaves;
        StartSpawner();
    }

    public void StartSpawner()
    {
        isSpawning = true;
    }

    private void Update()
    {
        if (waveCountdown <= 0)
        {
            if (spawnState != SpawnState.SPAWNING && !spawnerEmpty && isSpawning)
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
        spawnState = SpawnState.SPAWNING;

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
        spawnState = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWaveIndex + 1 > waves.Length - 1)
        {
            spawnerEmpty = true;
            Destroy(gameObject);
            //gameManager.spawnerDrained();
        }
        else
        {
            nextWaveIndex++;
        }

    }

    void SpawnEnemy(Transform enemy)
    {
        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, randomSpawn.position, randomSpawn.rotation);
    }

}
