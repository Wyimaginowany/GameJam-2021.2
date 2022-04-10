using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabulusWalk : StateMachineBehaviour
{
    Crabulus crabulusLogic;
    float timer;
    int random;
    int attacksDone = 0;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        random = Random.Range(0, 100);
        crabulusLogic = animator.GetComponent<Crabulus>();
        timer = crabulusLogic.walkingDuration;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (crabulusLogic.timesAttacked >= crabulusLogic.attacksBeforeRest)
        {
            animator.SetTrigger("rest");
        }
        else if (timer <= 0)
        {

            if (random < 50)
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
        if (crabulusLogic.timesAttacked >= crabulusLogic.attacksBeforeRest)
        {
            crabulusLogic.timesAttacked = 0;
            animator.ResetTrigger("rest");
        }
        else if (random < 50)
        {
            animator.ResetTrigger("normalAttack");
        }
        else
        {
            animator.ResetTrigger("circleAttack");
        }
    }

}
