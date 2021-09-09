using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] Weapon weapon;
    [SerializeField] Transform hand;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObjectsPool bulletsPool;

    [SerializeField] private float damageUpgrade = 0;
    [SerializeField] private float fireRateUpgrade = 0;

    float lastFired = 0f;

    private void Start()
    {
        weapon.SpawnWeapon(hand);
    }

    public void Shoot()
    {
        if (Time.time - lastFired > 1 / (weapon.fireRate + fireRateUpgrade))
        {
            lastFired = Time.time;
            var bullet = bulletsPool.GetBullet();
            bullet.GetComponent<Bullet>().SetBulletDamage(weapon.damage + damageUpgrade);
            bullet.transform.rotation = firePoint.transform.rotation;
            bullet.transform.position = firePoint.transform.position;
            bullet.gameObject.SetActive(true);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(firePoint.transform.up * weapon.bulletSpeed, ForceMode2D.Impulse);
        }     
    }

    public void Upgrade(UpgradeTypes type, float value)
    {
        switch(type)
        {
            case UpgradeTypes.Damage:
                damageUpgrade += value;
                break;
            case UpgradeTypes.FireRate:
                fireRateUpgrade += value;
                break;
            case UpgradeTypes.Health:
                //
                break;
        }
    }
}
