using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelobDeathrattle : MonoBehaviour, IDeathrattle
{
    [SerializeField] GameObject poolPrefab;
    [SerializeField] Transform spawnPoint;

    public void Deathrattle()
    {
        Instantiate(poolPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
