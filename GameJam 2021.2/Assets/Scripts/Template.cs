﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Template : MonoBehaviour
{
    [SerializeField] GameObject finalObject;
    [SerializeField] LayerMask trapsLayer;
    [SerializeField] int gridSize = 3;


    public void PlaceTrap()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.zero, Mathf.Infinity, trapsLayer);

        if (rayHit.collider == null)
        {
            Instantiate(finalObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void SnapToGrid(Vector2 mousePosition)
    {
        transform.position = new Vector2(
            Mathf.RoundToInt(mousePosition.x / gridSize) * gridSize,
            Mathf.RoundToInt(mousePosition.y / gridSize) * gridSize);
    }

}
