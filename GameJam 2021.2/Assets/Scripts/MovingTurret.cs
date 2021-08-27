using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTurret : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float movingSpeed = 200f;
    [SerializeField] float fireRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] float damage;
    [SerializeField] float period = 3f;
    [SerializeField] Vector3 movementVector;

    [Header("To Attach")]
    [SerializeField] GameObject movingParts;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform[] firePoints;

    GameManager gameManager;
    bool left = true;
    float lastFired = 0f;
    float movementFactor;
    Vector3 startingPos;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        startingPos = movingParts.transform.position;
    }

    void Update()
    {
        if (gameManager.GetCurrentState() == GameState.CombatPhase)
        {
            Shoot();
            MoveTurret();
        }
    }

    private void MoveTurret()
    {
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave / 2f + 0.5f;

        Vector3 offset = movementFactor * movementVector;
        movingParts.transform.position = startingPos + offset;
    }

    private void Shoot()
    {

        if (Time.time - lastFired > 1 / fireRate)
        {
            lastFired = Time.time;
            if (left)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoints[0].transform.position, firePoints[0].transform.rotation);
                ShootBullet(bullet, 0);
            }
            else
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoints[1].transform.position, firePoints[1].transform.rotation);
                ShootBullet(bullet, 1);
            }

            left = !left;
        }
    }

    private void ShootBullet(GameObject bullet, int gunIndex)
    {
        bullet.GetComponent<Bullet>().SetBulletDamage(damage);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce(firePoints[gunIndex].transform.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
