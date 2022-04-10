using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [SerializeField] float enemyHealth = 100f;

    [SerializeField] CircleCollider2D[] circleColliders;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] ParticleSystem deathParticle;

    Animator animator;
    AudioSource audioSource;
    Rigidbody2D rigidbody;
    private bool isAlive = true;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
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
        isAlive = false;
        if (deathParticle != null)  Instantiate(deathParticle, transform);
        Destroy(rigidbody);
        animator.SetTrigger("death");

        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            //audioSource.PlayOneShot(deathSound);
        }

        foreach (CircleCollider2D collider in circleColliders)
        {
            Destroy(collider);
        }

        if (TryGetComponent<IDeathrattle>(out var deathrattle))
        {
            deathrattle.Deathrattle();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
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
