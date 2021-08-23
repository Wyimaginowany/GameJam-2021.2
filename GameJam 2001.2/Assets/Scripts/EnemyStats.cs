using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] float enemyHealth = 100f;

    CircleCollider2D circleCollider;
    private bool isAlive = true;

    private void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

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
        Destroy(GetComponent<Collider>());
        Destroy(gameObject, 5f);
    }

    public bool CheckIfAlive()
    {
        return isAlive;
    }

}
