using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public GameObject weaponPrefab;
    public GameObject bulletPrefab;
    public float damage;
    public float bulletSpeed;
    public float fireRate;

    public void SpawnWeapon(Transform hand)
    {
        Instantiate(weaponPrefab, hand);
    }

}
