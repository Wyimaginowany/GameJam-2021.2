using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelobDeathrattle : MonoBehaviour
{
    [SerializeField] GameObject poolPrefab;
    [SerializeField] Transform spawnPoint;

    private void OnDestroy()
    {
        Instantiate(poolPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
