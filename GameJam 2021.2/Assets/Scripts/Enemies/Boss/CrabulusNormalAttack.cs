using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabulusNormalAttack : StateMachineBehaviour
{
    Crabulus crabulusLogic;
    float lastFired = 0f;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        crabulusLogic = animator.GetComponent<Crabulus>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time - lastFired > 1 / crabulusLogic.normalAttackFireRate)
        {
            lastFired = Time.time;
            crabulusLogic.NormalAttack();
        }
        crabulusLogic.RotateToPlayer();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        crabulusLogic.timesAttacked++;
    }

}
