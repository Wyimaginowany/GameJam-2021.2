using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    [SerializeField] float lifeDuration = 5f;

    GameManager gameManager;
    float timer;

    private void Awake()
    {
        transform.Rotate(0f, 0f, Random.Range(0f, 360f));
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeDuration || gameManager.GetCurrentState() != GameState.CombatPhase)
        {
            Destroy(gameObject);
        }
    }
}
