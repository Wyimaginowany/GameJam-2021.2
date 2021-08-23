using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 30f;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;

    [SerializeField] float fireRate = 5f;
    float lastFired = 0f;


    public void Shoot()
    {
        if (Time.time - lastFired > 1 / fireRate)
        {
            lastFired = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        }     
    }

}
