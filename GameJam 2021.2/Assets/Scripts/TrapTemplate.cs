using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTemplate : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] LayerMask trapsLayer;
    [SerializeField] int gridSize = 3;

    [Header("To Attach")]
    [SerializeField] GameObject finalObject;
    [SerializeField] GameObject objectsToRotate;

    float rotation = 0f;

    public void PlaceTrap()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, trapsLayer);

        if (rayHit.collider == null)
        {
            Instantiate(finalObject, transform.position, objectsToRotate.transform.rotation);
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

}
