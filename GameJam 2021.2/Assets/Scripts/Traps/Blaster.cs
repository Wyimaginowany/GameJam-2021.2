using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float fireRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] float damage;

    [Header("To Attach")]
    [SerializeField] GameObjectsPool bulletsPool;
    [SerializeField] Transform[] firePoints;
    [SerializeField] AudioClip shootSound;

    Animator animator;
    AudioSource audioSource;
    GameManager gameManager;
    float lastFired = 0f;
    int random = 0;

    private void Start()
    {
        var sameTraps = GameObject.FindGameObjectsWithTag("Blaster");
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
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time - lastFired > 1 / fireRate)
        {
            animator.SetTrigger("shoot");
            random = Random.Range(0, 2);
            Debug.Log(random);
            audioSource.PlayOneShot(shootSound);
            lastFired = Time.time;
            var bullet = bulletsPool.GetBullet();
            bullet.GetComponent<Bullet>().SetBulletDamage(damage);
            bullet.transform.rotation = firePoints[random].transform.rotation;
            bullet.transform.position = firePoints[random].transform.position;
            bullet.gameObject.SetActive(true);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(firePoints[random].transform.up * bulletSpeed, ForceMode2D.Impulse);

        }
    }
}
