using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject shop;
    //to do make it automatic
    [SerializeField] Camera camera = null;

    GameManager gameManager;
    GameObject selectedTrap = null;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    Vector2 movement;
    Vector2 mousePosition;
    bool isPaused = false;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponent<PlayerShooting>();
        gameManager.SetState(GameState.CombatPhase);
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        /*if (gameManager.GetCurrentState() == GameState.WaitingPhase)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gameManager.SetState(GameState.CombatPhase);
            }
        }*/

        if (gameManager.GetCurrentState() == GameState.CombatPhase)
        {
            if (Input.GetMouseButton(0) && !isPaused)
            {
                playerShooting.Shoot();
            }
        }
        else if (gameManager.GetCurrentState() == GameState.BuildPhase && !isPaused)
        {
            if (selectedTrap != null)
            {
                selectedTrap.GetComponent<TrapTemplate>().SnapToGrid(mousePosition);
                //this can stay i think (only minor changes)
                if (Input.GetKeyDown(KeyCode.R))
                {
                    selectedTrap.GetComponent<TrapTemplate>().Rotate();
                }
                if (Input.GetMouseButtonDown(0))
                {
                    selectedTrap.GetComponent<TrapTemplate>().PlaceTrap(shop);
                }
            }           
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.GetCurrentState() == GameState.CombatPhase)
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

    public void SelectTrap(GameObject trapTemplate)
    {
        Destroy(selectedTrap);
        selectedTrap = Instantiate(trapTemplate, transform.position, Quaternion.identity);
    }

    public void Resume()
    {
        if (gameManager.GetCurrentState() == GameState.BuildPhase && selectedTrap == null)
        {
            shop.SetActive(true);
        }
        if (!gameManager.isOver())
        {
            Time.timeScale = 1f;
        }

        pauseMenu.GetComponent<PauseMenu>().RefreshPauseMenu();
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        shop.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
