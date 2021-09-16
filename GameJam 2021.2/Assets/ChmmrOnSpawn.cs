using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChmmrOnSpawn : MonoBehaviour
{
    [SerializeField] float idleDuration = 2f;

    EnemyMovement enemyMovement;
    Animator animator;

    // remove serialize
    [SerializeField] float timer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= idleDuration)
        {
            animator.SetTrigger("move");
            enemyMovement.ResetSpeed();
        }
        else
        {
            enemyMovement.SetEnemySpeed(0);
        }
    }
}
