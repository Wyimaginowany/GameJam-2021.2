using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnState {SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public Transform enemyPrefab;
        public int count;
        public float spawnRate;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    private int nextWave = 0;
    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    private SpawnState spawnState = SpawnState.COUNTING;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (waveCountdown <= 0)
        {
            if (spawnState != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        spawnState = SpawnState.SPAWNING;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        WaveCompleated();
        yield break;
    }

    void WaveCompleated()
    {
        spawnState = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLEATE");
        }
        else
        {
            nextWave++;
        }

    }

    void SpawnEnemy(Transform enemy)
    {
        Transform randomSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, randomSpawn.position, randomSpawn.rotation);
    }

}
