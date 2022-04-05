﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [SerializeField] float enemyHealth = 100f;

    [SerializeField] CircleCollider2D[] circleColliders;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;

    AudioSource audioSource;
    Rigidbody2D rigidbody;
    private bool isAlive = true;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        
        if (enemyHealth <= 0)
        {
            HandleDeath();
        }
        else if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void HandleDeath()
    {
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        isAlive = false;
        Destroy(rigidbody);
        foreach (CircleCollider2D collider in circleColliders)
        {
            Destroy(collider);
        }

        if (TryGetComponent<IDeathrattle>(out var deathrattle))
        {
            deathrattle.Deathrattle();
        }
        Destroy(gameObject, deathSound.length);
    }

    public bool CheckIfAlive()
    {
        return isAlive;
    }

}

public interface IDeathrattle
{
    void Deathrattle();
}
