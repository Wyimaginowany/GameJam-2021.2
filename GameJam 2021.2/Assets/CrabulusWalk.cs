using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabulusWalk : StateMachineBehaviour
{
    Crabulus crabulusLogic;
    float timer;
    int random;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        random = Random.Range(0, 2);
        Debug.Log(random);
        crabulusLogic = animator.GetComponent<Crabulus>();
        timer = 5f;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            if (random == 0)
            {
                animator.SetTrigger("normalAttack");
            }
            else
            {
                animator.SetTrigger("circleAttack");
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
        crabulusLogic.MoveToPlayer();
        crabulusLogic.RotateToPlayer();
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {    
        if (random == 0)
        {
            animator.ResetTrigger("normalAttack");
        }
        else
        {
            animator.ResetTrigger("circleAttack");
        }
    }

}
