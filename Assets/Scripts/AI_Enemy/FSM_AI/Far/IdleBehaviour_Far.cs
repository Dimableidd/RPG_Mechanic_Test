using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour_Far : FSM_Base_Far
{
    float timer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo,layerIndex);
        timer = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if(timer > 5) animator.SetBool("isPatrolling", true);

        float distance = Vector3.Distance(animator.transform.position, player.transform.position);

        if (distance < aggroRange)
            animator.SetBool("isChasing", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}