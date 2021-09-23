using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentDeathrattle : MonoBehaviour
{
    [SerializeField] GameObject[] childPrefab;
    [SerializeField] GameObject[] spawnPoints;

    private void OnDestroy()
    {
        for (int i = 0; i < childPrefab.Length; i++)
        {
            Instantiate(childPrefab[i], spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
        }
    }
}
