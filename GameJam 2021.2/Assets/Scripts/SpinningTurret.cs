using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningTurret : MonoBehaviour
{
    [SerializeField] float spiningSpeed = 200f;
    [SerializeField] float fireRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] float damage;
    [SerializeField] GameObject movingParts;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    float lastFired = 0f;

    private void Update()
    {
        movingParts.transform.Rotate(new Vector3(0f, 0f, spiningSpeed) * Time.deltaTime);
        Shoot();
    }

    public void Shoot()
    {
        if (Time.time - lastFired > 1 / fireRate)
        {
            lastFired = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
            bullet.GetComponent<Bullet>().SetBulletDamage(damage);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(firePoint.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
