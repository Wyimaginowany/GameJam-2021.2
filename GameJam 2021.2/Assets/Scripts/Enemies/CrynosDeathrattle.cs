using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrynosDeathrattle : MonoBehaviour, IDeathrattle
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform spawnPoint;

    public void Deathrattle()
    {
        FireBullet(Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity), Vector3.left);
        FireBullet(Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity), Vector3.right);
        FireBullet(Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity), Vector3.down);
        FireBullet(Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity), Vector3.up);

        FireBullet(Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity), new Vector3(-1f,-1f,0f).normalized);
        FireBullet(Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity), new Vector3(-1f, 1f, 0f).normalized);
        FireBullet(Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity), new Vector3(1f, 1f, 0f).normalized);
        FireBullet(Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity), new Vector3(1f, -1f, 0f).normalized);

    }

    private void FireBullet(GameObject bullet, Vector3 direction)
    {
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
    }
}
