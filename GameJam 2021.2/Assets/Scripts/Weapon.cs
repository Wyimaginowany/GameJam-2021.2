using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Settings")]
    public float damage;
    public float bulletSpeed;
    public float fireRate;

    [Header("To Attach")]
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;

    public void SpawnWeapon(Transform hand)
    {
        Instantiate(weaponPrefab, hand);
    }

}
