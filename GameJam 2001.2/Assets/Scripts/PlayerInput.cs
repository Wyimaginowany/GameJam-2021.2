using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Camera camera = null;

    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    Vector2 movement;
    Vector2 mousePosition;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponent<PlayerShooting>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            playerShooting.Shoot();
        }
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
