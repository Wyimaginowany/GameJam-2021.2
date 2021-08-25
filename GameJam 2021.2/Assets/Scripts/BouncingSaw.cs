using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingSaw : MonoBehaviour
{
    [SerializeField] float spiningSpeed = 330;
    [SerializeField] float randomFactor = 0.2f;
    [SerializeField] Vector2 startForce = new Vector2(5f, 3f);

    [SerializeField] GameObject sawGFX;

    Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity += startForce;
    }

    private void Update()
    {
        sawGFX.transform.Rotate(new Vector3(0f, 0f, spiningSpeed) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyStats>().HandleHit(100f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Vector2 velocityTweak = new Vector2
        (Random.Range(-randomFactor, randomFactor),
         Random.Range(-randomFactor, randomFactor));

        rigidbody.velocity += velocityTweak;
    }
}
