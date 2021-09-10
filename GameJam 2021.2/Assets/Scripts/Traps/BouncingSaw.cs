using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingSaw : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float damage = 100f;
    [SerializeField] float speed = 15f;
    [SerializeField] float spiningSpeed = 330f;
    [SerializeField] float randomFactor = 0.2f;

    [Header("To Attach")]
    [SerializeField] GameObject sawGFX;

    bool gameStarted = false;
    GameManager gameManager;
    Vector2 startForce = new Vector2(5f, 3f);
    Rigidbody2D rigidbody;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (gameManager.GetCurrentState() == GameState.CombatPhase)
        {
            sawGFX.transform.Rotate(new Vector3(0f, 0f, spiningSpeed) * Time.deltaTime);
            if (!gameStarted)
            {
                startForce = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f)).normalized * speed;
                rigidbody.velocity += startForce;
                gameStarted = true;
            }
        }
        else
        {
            gameStarted = false;
            transform.position = new Vector3(0, 0, 0);
            rigidbody.velocity = new Vector3(0, 0, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyStats>().HandleHit(damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Vector2 velocityTweak = new Vector2
        (Random.Range(-randomFactor, randomFactor),
         Random.Range(-randomFactor, randomFactor));
        spiningSpeed = -spiningSpeed;

        rigidbody.velocity += velocityTweak;
    }
}
