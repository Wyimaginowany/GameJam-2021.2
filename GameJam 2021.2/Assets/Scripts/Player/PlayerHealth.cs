using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float invincibleDuration = 1f;
    [SerializeField] int playerMaxHealth = 5;
    [SerializeField] PlayerUI playerUI;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] AudioClip hitSound;

    GameManager gameManager;
    AudioSource audioSource;
    public bool invincible = false;
    int playerHealth = 5;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        playerHealth = playerMaxHealth;
        playerUI.RefreshHealth(playerMaxHealth, playerHealth);
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("EnemyBullet"))
        {
            TakeDamege();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pool"))
        {
            TakeDamege();
        }
    }

    public void TakeDamege()
    {
        if (!invincible)
        {
            audioSource.PlayOneShot(hitSound);
            playerHealth--;
            playerUI.RefreshHealth(playerMaxHealth, playerHealth);
            invincible = true;
            if (playerHealth <= 0)
            {
                HandleDeath();
            }
            else
            {
                Invoke("ActiveCollider", invincibleDuration);
                //make it so the player can see that he is invincible
                //turn off collider for invincibleDuration
            }
        }

    }

    private void HandleDeath()
    {
        gameManager.GameOver();
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void ActiveCollider()
    {
        invincible = false;
    }

    public void Upgrade(int value)
    {
        playerMaxHealth++;
        playerHealth = playerMaxHealth;
    }

    public int GetMaxHealth()
    {
        return playerMaxHealth;
    }

    public int GetCurrentHealth()
    {
        return playerHealth;
    }
}
