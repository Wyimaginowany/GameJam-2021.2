using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] float enemyHealth = 100f;

    [SerializeField] CircleCollider2D[] circleColliders;


    Rigidbody2D rigidbody;
    private bool isAlive = true;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
        Destroy(rigidbody);
        foreach (CircleCollider2D collider in circleColliders)
        {
            Destroy(collider);
        }

        Destroy(gameObject);
    }

    public bool CheckIfAlive()
    {
        return isAlive;
    }


}
