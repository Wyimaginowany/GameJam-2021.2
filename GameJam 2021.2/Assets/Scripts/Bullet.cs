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
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
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
