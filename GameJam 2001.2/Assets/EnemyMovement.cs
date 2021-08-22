using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float enemySpeed = 2f;
    Rigidbody2D rigidbody;


    private Transform target;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        Vector2 destination = target.transform.position - transform.position;
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        //rigidbody.velocity = destination.normalized * enemySpeed * Time.deltaTime;
        rigidbody.MovePosition(currentPos + destination.normalized * enemySpeed * Time.deltaTime);
       //transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
    }

}
