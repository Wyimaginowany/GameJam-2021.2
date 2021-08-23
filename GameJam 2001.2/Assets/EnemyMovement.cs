using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float enemySpeed = 2f;
    Rigidbody2D rigidbody;
    EnemyStats enemyStats;

    private Transform target;

    private void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (enemyStats.CheckIfAlive())
        {
            MoveToPlayer();
        }
    }

    private void MoveToPlayer()
    {
        Vector2 destination = target.transform.position - transform.position;
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        rigidbody.MovePosition(currentPos + destination.normalized * enemySpeed * Time.deltaTime);
    }
}
