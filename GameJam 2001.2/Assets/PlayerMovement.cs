using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;

    Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void MovePlayer(Vector2 movement)
    {
        rigidbody.MovePosition(rigidbody.position + movement * movementSpeed * Time.deltaTime);
    }

    public void RotatePlayer(Vector2 mousePosition)
    {
        Vector2 lookDirection = mousePosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rigidbody.rotation = angle;
    }

}
