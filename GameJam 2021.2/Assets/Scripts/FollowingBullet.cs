using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingBullet : MonoBehaviour
{
    private float damage;
    private float speed;
    public GameObject enemy;

    private void Update()
    {
        if (enemy != null)
        {
            Vector3 direction = enemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }

        transform.position += speed * Time.deltaTime * transform.up;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<EnemyStats>().HandleHit(damage);
        }

        Destroy(gameObject);
    }

    public void CreateBullet(float damage, float speed, GameObject enemy)
    {
        this.damage = damage;
        this.speed = speed;
        this.enemy = enemy;
    }
}
