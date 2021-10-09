using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crabulus : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float heatlh;
    [SerializeField] float speed;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] int attacksBeforeRest = 3;
    [Header("To attach")]
    [SerializeField] GameObject[] circleFirePoints;
    [SerializeField] GameObject[] normalFirePoints;
    [SerializeField] GameObject rotatingParts;
    [SerializeField] GameObjectsPool bulletsPool;
    [Header("Crircle Attack")]
    [SerializeField] float wavingPeriod = 10f;

    Transform player;
    Vector2 destination;
    Rigidbody2D rigidbody;
    float movementFactor;
    int timesAttacked;

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
            bulletRigidbody.AddForce(firePoint.transform.up * bulletSpeed, ForceMode2D.Force);
        }
    }

    public void Shoot()
    {
        foreach (GameObject firePoint in circleFirePoints)
        {
            var bullet = bulletsPool.GetBullet();
            bullet.transform.rotation = firePoint.transform.rotation;
            bullet.transform.position = firePoint.transform.position;
            bullet.gameObject.SetActive(true);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.AddForce(firePoint.transform.up * bulletSpeed, ForceMode2D.Force);
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

    public void EndAttack()
    {
        timesAttacked++;
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
