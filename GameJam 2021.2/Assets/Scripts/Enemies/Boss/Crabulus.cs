﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crabulus : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float heatlh;
    [SerializeField] float speed;
    [SerializeField] float bulletSpeed = 10f;
    public int attacksBeforeRest = 3;
    [Header("To attach")]
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject[] normalFirePoints;
    [SerializeField] GameObject rotatingParts;
    [SerializeField] GameObjectsPool bulletsPool;
    [Space(10)]
    [SerializeField] float wavingPeriod = 10f;
    public float circleAttackDuration;
    public float walkingDuration;
    public float restingDuration;
    public float normalAttackFireRate;


    Transform player;
    Vector2 destination;
    Rigidbody2D rigidbody;
    float movementFactor;
    public int timesAttacked;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timesAttacked = 0;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void NormalAttack()
    {
        foreach (GameObject firePoint in normalFirePoints)
        {
            var bullet = bulletsPool.GetBullet();
            bullet.transform.rotation = firePoint.transform.rotation;
            bullet.transform.position = firePoint.transform.position;
            bullet.gameObject.SetActive(true);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(firePoint.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    public void Shoot()
    {
        for (int i =0; i < 16; i++)
        {
            var bullet = bulletsPool.GetBullet();
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.rotation = firePoint.transform.rotation * Quaternion.Euler(0f, 0f, i*22.5f);
            bullet.gameObject.SetActive(true);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    public void Wave()
    {
        float cycles = Time.time / wavingPeriod;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f;

        rotatingParts.transform.eulerAngles = new Vector3(
            rotatingParts.transform.eulerAngles.x,
            rotatingParts.transform.eulerAngles.y,
            movementFactor * 90);
    }


    public void MoveToPlayer()
    {
        destination = player.position - transform.position;
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        rigidbody.MovePosition(currentPos + destination.normalized * speed * Time.fixedDeltaTime);
    }

    public void RotateToPlayer()
    {
        var destination = player.position - transform.position;
        float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg - 90f;
        rigidbody.rotation = angle;
    }
}