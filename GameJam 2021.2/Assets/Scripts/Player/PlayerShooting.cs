using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] Weapon weapon;
    [SerializeField] Transform hand;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObjectsPool bulletsPool;

    private float damageUpgrade = 0;
    private float fireRateUpgrade = 0;

    Animator animator;
    PlayerHealth playerHealth;
    float lastFired = 0f;
    AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        weapon.SpawnWeapon(hand);
        playerHealth = GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        if (Time.time - lastFired > 1 / (weapon.fireRate + fireRateUpgrade))
        {
            animator.SetTrigger("shoot");
            lastFired = Time.time;
            var bullet = bulletsPool.GetBullet();
            bullet.GetComponent<Bullet>().SetBulletDamage(weapon.damage + damageUpgrade);
            bullet.transform.rotation = firePoint.transform.rotation;
            bullet.transform.position = firePoint.transform.position;
            bullet.gameObject.SetActive(true);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(firePoint.transform.up * weapon.bulletSpeed, ForceMode2D.Impulse);
            audioSource.PlayOneShot(weapon.shootSound);
        }     
    }

    public void Upgrade(UpgradeTypes type, float value, float secondValue)
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
                playerHealth.Upgrade((int)value);
                break;
            case UpgradeTypes.DamageFireRate:
                damageUpgrade += value;
                fireRateUpgrade += secondValue;
                break;
        }
    }

    public float GetCombinedDamage()
    {
        return weapon.damage + damageUpgrade;
    }

    public float GetCombinedFireRate()
    {
        return weapon.fireRate + fireRateUpgrade;
    }
}
