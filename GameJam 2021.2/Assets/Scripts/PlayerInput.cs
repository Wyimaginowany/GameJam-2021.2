using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //to do make it automatic
    [SerializeField] Camera camera = null;

    //todo remove this from input and make it automatic
    [SerializeField] GameObject[] traps;

    GameObject selectedTrap;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    Vector2 movement;
    Vector2 mousePosition;
    bool combatPhase = true;

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

        //make this automatic (shop)
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            combatPhase = !combatPhase;
        }
        
        if (combatPhase)
        {
            if (Input.GetMouseButton(0))
            {
                playerShooting.Shoot();
            }
        }
        else
        {
            //todo remove this from input and make it automatic (shop)
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedTrap = Instantiate(traps[0], transform.position, Quaternion.identity);
            }
            
            if (selectedTrap != null)
            {
                selectedTrap.GetComponent<Template>().SnapToGrid(mousePosition);
                if (Input.GetKeyDown(KeyCode.R))
                {
                    selectedTrap.GetComponent<Template>().Rotate();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    selectedTrap.GetComponent<Template>().PlaceTrap();
                }
            }           
        }
    }

    private void FixedUpdate()
    {
        if (combatPhase)
        {
            ApplyPlayerMovement();
            playerMovement.RotatePlayer(mousePosition);
        }
    }

    private void ApplyPlayerMovement()
    {
        if (movement.magnitude >= 0.1f)
        {
            playerMovement.MovePlayer(movement.normalized);
        }
        else
        {
            playerMovement.MovePlayer(Vector3.zero);
        }
    }
}
