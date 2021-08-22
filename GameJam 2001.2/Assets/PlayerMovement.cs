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

}
