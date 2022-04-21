using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float spiningSpeed = 200f;
    [SerializeField] float fireRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] float damage;

    [Header("To Attach")]
    [SerializeField] GameObjectsPool bulletsPool;
    [SerializeField] GameObject movingParts;
    [SerializeField] Transform firePoint;
    [SerializeField] AudioClip shootSound;

    Animator animator;
    AudioSource audioSource;
    GameManager gameManager;
    float lastFired = 0f;
    bool oddShot = true;

    private void Start()
    {
        var sameTraps = GameObject.FindGameObjectsWithTag("Spinner");
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        if (sameTraps.Length > 1)
        {
            audioSource.enabled = false;
        }
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.GetCurrentState() == GameState.CombatPhase)
        {
            movingParts.transform.Rotate(new Vector3(0f, 0f, spiningSpeed) * Time.deltaTime);
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
            bulletRigidbody.velocity = Vector3.zero;
            bulletRigidbody.angularVelocity = 0;
            bulletRigidbody.AddForce(firePoint.transform.up * bulletSpeed, ForceMode2D.Impulse);

        }
    }
}
