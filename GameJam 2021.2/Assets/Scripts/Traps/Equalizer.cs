using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equalizer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float fireRate;
    [SerializeField] float bulletSpeed;
    [SerializeField] float damage;

    [Header("To Attach")]
    [SerializeField] GameObject movingParts;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    GameManager gameManager;
    GameObject player;
    GameObject[] enemies;
    float lastFired = 0f;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = gameManager.GetPlayer();
    }

    private void Update()
    {
        if (gameManager.GetCurrentState() == GameState.CombatPhase)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            GameObject target = GetClosestEnemy(GameObject.FindGameObjectWithTag("Player").transform, enemies);
            if (target != null)
            {
                Rotate(target);
                Shoot(target);
            }

        }
    }

    private void Shoot(GameObject target)
    {
        if (Time.time - lastFired > 1 / fireRate)
        {
            lastFired = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
            bullet.GetComponent<FollowingBullet>().CreateBullet(damage, bulletSpeed, target);  
        }
    }

    private void Rotate(GameObject enemy)
    {
        Vector3 direction = enemy.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        movingParts.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    GameObject GetClosestEnemy(Transform player, GameObject[] enemies)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 playerPosition = player.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - playerPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}
