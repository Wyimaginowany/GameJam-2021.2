using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waver : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float wavingPeriod = 10f;
    [SerializeField] float fireRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] float damage;
    [SerializeField] float startingAngle;

    [Header("To Attach")]
    [SerializeField] GameObjectsPool bulletsPool;
    [SerializeField] GameObject movingParts;
    [SerializeField] Transform firePoint;
    [SerializeField] AudioClip shootSound;

    Animator animator;
    AudioSource audioSource;
    GameManager gameManager;
    float lastFired = 0f;
    float movementFactor;

    private void Start()
    {
        var sameTraps = GameObject.FindGameObjectsWithTag("Waver");
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        if (sameTraps.Length > 1)
        {
            audioSource.enabled = false;
        }
        startingAngle = transform.rotation.eulerAngles.z;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.GetCurrentState() == GameState.CombatPhase)
        {
            Wave();
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.GetCurrentState() == GameState.CombatPhase)
        {
            Shoot();
        }
    }


    private void Shoot()
    {
        if (Time.time - lastFired > 1 / fireRate)
        {
            animator.SetTrigger("shoot");
            audioSource.PlayOneShot(shootSound);
            lastFired = Time.time;
            var bullet = bulletsPool.GetBullet();
            bullet.GetComponent<Bullet>().SetBulletDamage(damage);
            bullet.transform.rotation = firePoint.transform.rotation;
            bullet.transform.position = firePoint.transform.position;
            bullet.gameObject.SetActive(true);
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
