using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IGameObjectPolled
{
    private float damage;

    private GameObjectsPool pool;
    public GameObjectsPool Pool
    {
        get { return pool;  }
        set
        {
            if (pool == null)
            {
                pool = value;
            }
            else
            {
                throw new System.Exception("Pool is used wrong way");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            collision.GetComponent<IDamageable>().TakeDamage(damage);
        }
        /*if (collision.collider.CompareTag("Enemy"))
        {
            //collision.collider.GetComponent<EnemyStats>().HandleHit(damage);
            collision.collider.GetComponent<IDamageable>().TakeDamage(damage);
            //collision.collider.GetComponent<IDamageable>().HandleHit(damage);
        }*/
        pool.ReturnToPool(this.gameObject);
    }

    public void SetBulletDamage(float damage)
    {
        this.damage = damage;
    }

}

internal interface IGameObjectPolled
{
    GameObjectsPool Pool { get; set; }
}
