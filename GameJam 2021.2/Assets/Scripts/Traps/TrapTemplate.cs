using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTemplate : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] string trapName;
    public bool isPlaceable = true;
    [SerializeField] LayerMask trapsLayer;
    [SerializeField] int trapPrice = 50;

    [Header("To Attach")]
    [SerializeField] GameObject finalObject;
    [SerializeField] GameObject objectsToRotate;
    [SerializeField] GameObject trapIcon;

    int gridSize;
    float rotation = 0f;

    private void Start()
    {
        gridSize = GameObject.Find("GameManager").GetComponent<GameManager>().GetGridSize();
    }

    public void PlaceTrap(GameObject shop)
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, trapsLayer);

        if (rayHit.collider == null)
        {
            Instantiate(finalObject, transform.position, objectsToRotate.transform.rotation);
            shop.SetActive(true);
            Destroy(gameObject);
        }
    }

    public void SnapToGrid(Vector2 mousePosition)
    {
        transform.position = new Vector2(
            Mathf.RoundToInt(mousePosition.x / gridSize) * gridSize,
            Mathf.RoundToInt(mousePosition.y / gridSize) * gridSize);
    }

    public void Rotate()
    {
        rotation -= 90f;
        objectsToRotate.transform.localRotation = Quaternion.Euler(0f, 0f, rotation);
    }

    public string GetTrapName()
    {
        return trapName;
    }

    public int GetTrapPrice()
    {
        return trapPrice;
    }

    public GameObject GetTrapIcon()
    {
        return trapIcon;
    }

    public void SpawnTrap()
    {
        Vector3 spawnPosition = new Vector3(0f, 0f, 0f);
        Instantiate(finalObject, spawnPosition, Quaternion.identity);
    }
}
