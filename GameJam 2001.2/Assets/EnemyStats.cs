using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] float enemyHealth = 100f;
    [SerializeField] CircleCollider2D collider;

    private bool isAlive = true;

    public void HandleHit(float damage)
    {
        enemyHealth -= damage;
        
        if (enemyHealth <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        isAlive = false;
        Destroy(collider);
        Destroy(gameObject, 5f);
    }

    public bool CheckIfAlive()
    {
        return isAlive;
    }

}
