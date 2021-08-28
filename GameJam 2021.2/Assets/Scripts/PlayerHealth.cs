using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float invincibleDuration = 1f;


    bool invincible = false;
    int playerHealth = 5;


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && !invincible)
        {
            TakeDamege();
        }
    }


    public void TakeDamege()
    {
        playerHealth--;
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



}
