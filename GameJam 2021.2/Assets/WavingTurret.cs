using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavingTurret : MonoBehaviour
{
    [SerializeField] float wavingPeriod = 10f;
    [SerializeField] float fireRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] float damage;
    [SerializeField] float startingAngle;
    [SerializeField] GameObject movingParts;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    float lastFired = 0f;
    float movementFactor;

    private void Start()
    {
        startingAngle = transform.rotation.eulerAngles.z;
    }

    private void Update()
    {
        Wave();
        Shoot();
    }

    private void Shoot()
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

    private void Wave()
    {
        float cycles = Time.time / wavingPeriod;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f;

        movingParts.transform.eulerAngles = new Vector3(
            movingParts.transform.eulerAngles.x,
            movingParts.transform.eulerAngles.y,
            startingAngle + movementFactor * 90 - 45);
    }
}
