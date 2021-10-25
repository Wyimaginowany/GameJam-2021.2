using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabulusCircleAttack : StateMachineBehaviour
{
    Crabulus crabulusLogic;
    float timer;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        crabulusLogic = animator.GetComponent<Crabulus>();
        timer = crabulusLogic.circleAttackDuration;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("endAttack");
        }
        else
        {
            timer -= Time.deltaTime;
        }
        crabulusLogic.Wave();
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        crabulusLogic.timesAttacked++;
        animator.ResetTrigger("endAttack");
    }
}
