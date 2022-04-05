using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingBullet : MonoBehaviour
{
    private float speed;
    GameObject enemy;

    private void Update()
    {
        if (enemy != null)
        {
            if (enemy.GetComponent<EnemyStats>().CheckIfAlive())
            {
                Vector3 direction = enemy.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }
        
        transform.position += speed * Time.deltaTime * transform.up;
    }

    public void CreateBullet(float speed, GameObject enemy)
    {
        this.speed = speed;
        this.enemy = enemy;
    }
}
