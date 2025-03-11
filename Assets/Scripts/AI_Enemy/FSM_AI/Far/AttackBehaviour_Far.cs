using System.Collections;
using System.Collections.Generic;
using Game.Entities;
using UnityEngine;

public class AttackBehaviour_Far : FSM_Base_Far
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo,layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player.transform);
        float distance = Vector3.Distance(animator.transform.position, player.transform.position);

        if (distance > attackRange)
            animator.SetBool("isAttacking", false);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
