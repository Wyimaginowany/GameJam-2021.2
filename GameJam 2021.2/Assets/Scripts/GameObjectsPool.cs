using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsPool : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    public static GameObjectsPool Instance { get; private set; }
    private Queue<GameObject> bullets = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
    }


    public GameObject GetBullet()
    {
        if (bullets.Count == 0)
        {
            AddBullet(1);
        }
        return bullets.Dequeue();
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
        bullets.Enqueue(objectToReturn);
    }

    private void AddBullet(int amount)
    {
        var newBullet = GameObject.Instantiate(bulletPrefab);
        newBullet.SetActive(false);
        bullets.Enqueue(newBullet);

        newBullet.GetComponent<IGameObjectPolled>().Pool = this;
    }
}
