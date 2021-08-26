using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private enum SpawnState {SPAWNING, COUNTING};

    [SerializeField] Wave[] waves;
    [Space(10)]
    [SerializeField] Transform[] spawnPoints;
    [Space(10)]
    [SerializeField] float timeBetweenWaves = 5f;

    private int nextWaveIndex = 0;
    private float waveCountdown;
    private SpawnState spawnState = SpawnState.COUNTING;

    [System.Serializable]
    public class Wave
    {
        public Transform enemyPrefab;
        public int amount;
        public float spawnRate;
    }

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (waveCountdown <= 0 && spawnState != SpawnState.SPAWNING)
        {
            StartCoroutine(SpawnWave(waves[nextWaveIndex]));
        }
        else if (waveCountdown > 0)
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
            nextWaveIndex = 0;
            Debug.Log("ALL WAVES COMPLEATE");
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
