using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<EnemyStats>().HandleHit(damage);
        }

        Destroy(gameObject);
    }

    public void SetBulletDamage(float damage)
    {
        this.damage = damage;
    }

}
