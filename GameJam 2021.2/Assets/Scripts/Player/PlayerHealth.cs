using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float invincibleDuration = 1f;
    [SerializeField] int playerMaxHealth = 5;
    [SerializeField] PlayerUI playerUI;

    bool invincible = false;
    int playerHealth = 5;

    private void Start()
    {
        playerHealth = playerMaxHealth;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && !invincible)
        {
            TakeDamege();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pool") && !invincible)
        {
            TakeDamege();
        }
    }

    public void TakeDamege()
    {
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

    private void HandleDeath()
    {
        //destroy player
        //show death ui
        //stop all enemies and animations
        Debug.Log("player died");
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
