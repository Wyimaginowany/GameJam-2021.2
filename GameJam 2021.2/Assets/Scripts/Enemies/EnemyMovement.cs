using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float startingSpeed;

    
    float enemySpeed;
    Rigidbody2D rigidbody;
    EnemyStats enemyStats;
    private Transform target;

    private void Start()
    {
        enemySpeed = startingSpeed;
        enemyStats = GetComponent<EnemyStats>();
        rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if (enemyStats.CheckIfAlive())
        {
            Vector2 destination = target.transform.position - transform.position;
            MoveToPlayer(destination);
            RotateToPlayer(destination);
        }
    }

    private void MoveToPlayer(Vector2 destination)
    {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        rigidbody.MovePosition(currentPos + destination.normalized * enemySpeed * Time.deltaTime);
    }

    private void RotateToPlayer(Vector2 destination)
    {
        float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg - 90f;
        rigidbody.rotation = angle;
    }

    public void SetEnemySpeed(float speed)
    {
        enemySpeed = speed;
    }

    public void ResetSpeed()
    {
        enemySpeed = startingSpeed;
    }
}
