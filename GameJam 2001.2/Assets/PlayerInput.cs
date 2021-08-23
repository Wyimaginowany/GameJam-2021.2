using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Camera camera = null;

    PlayerMovement playerMovement;
    Vector2 movement;
    Vector2 mousePosition;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        if (movement.magnitude >= 0.1f)
        {
            playerMovement.MovePlayer(movement.normalized);
        }
        else
        {
            playerMovement.MovePlayer(Vector3.zero);
        }

        playerMovement.RotatePlayer(mousePosition);
    }
}
