using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour, IGameObjectPolled
{
    private GameObjectsPool pool;
    public GameObjectsPool Pool
    {
        get { return pool; }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamege();
        }
        pool.ReturnToPool(this.gameObject);
    }

}
