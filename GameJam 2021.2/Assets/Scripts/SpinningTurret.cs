using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningTurret : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float spiningSpeed = 200f;
    [SerializeField] float fireRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] float damage;

    [Header("To Attach")]
    [SerializeField] GameObject movingParts;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    GameManager gameManager;
    float lastFired = 0f;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.GetCurrentState() == GameState.CombatPhase)
        {
            movingParts.transform.Rotate(new Vector3(0f, 0f, spiningSpeed) * Time.deltaTime);
            Shoot();
        }
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
}
