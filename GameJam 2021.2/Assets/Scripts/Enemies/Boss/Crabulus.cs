using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Crabulus : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] float health;
    [SerializeField] float speed;
    [SerializeField] float bulletSpeed = 10f;
    public int attacksBeforeRest = 3;
    [Header("To attach")]
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject[] normalFirePoints;
    [SerializeField] GameObject rotatingParts;
    [SerializeField] GameObjectsPool bulletsPool;
    [SerializeField] Slider healtBar;
    [SerializeField] TMP_Text healthBarText;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shootSound;
    [Space(10)]
    [SerializeField] float wavingPeriod = 10f;
    public float circleAttackDuration;
    public float walkingDuration;
    public float restingDuration;
    public float normalAttackFireRate;
    float maxHealth;

    Animator animator;
    Transform player;
    Vector2 destination;
    Rigidbody2D rigidbody;
    float movementFactor;
    public int timesAttacked;
    AudioSource audioSource;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        timesAttacked = 0;
        rigidbody = GetComponent<Rigidbody2D>();
        maxHealth = health;
        healtBar.maxValue = maxHealth;
        healtBar.value = health;
        healthBarText.text = health + " / " + maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    public void NormalAttack()
    {
        foreach (GameObject firePoint in normalFirePoints)
        {
            audioSource.PlayOneShot(shootSound);
            var bullet = bulletsPool.GetBullet();
            bullet.transform.rotation = firePoint.transform.rotation;
            bullet.transform.position = firePoint.transform.position;
            bullet.gameObject.SetActive(true);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(firePoint.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    public void Shoot()
    {
        audioSource.PlayOneShot(shootSound);
        for (int i =0; i < 16; i++)
        {
            var bullet = bulletsPool.GetBullet();
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = firePoint.transform.rotation * Quaternion.Euler(0f, 0f, i*22.5f);
            bullet.gameObject.SetActive(true);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    public void Wave()
    {
        float cycles = Time.time / wavingPeriod;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f;

        rotatingParts.transform.eulerAngles = new Vector3(
            rotatingParts.transform.eulerAngles.x,
            rotatingParts.transform.eulerAngles.y,
            movementFactor * 90);
    }


    public void MoveToPlayer()
    {
        destination = player.position - transform.position;
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        rigidbody.MovePosition(currentPos + destination.normalized * speed * Time.fixedDeltaTime);
    }

    public void RotateToPlayer()
    {
        var destination = player.position - transform.position;
        float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg - 90f;
        rigidbody.rotation = angle;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            HandleDeath();
            health = 0;
        }

        healtBar.value = health;
        healthBarText.text = health + " / " + maxHealth;
    }

    private void HandleDeath()
    {
        animator.SetTrigger("death");
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    private void DestroyBoss()
    {
        Destroy(gameObject);
    }
}


public interface IDamageable
{
    void TakeDamage(float amount);
}
