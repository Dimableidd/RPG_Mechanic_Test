using Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_Base : StateMachineBehaviour
{
    protected float Damage = 1f;
    protected float attackRange = 1.5f;
    protected float aggroRange = 5f;

    protected GameObject player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<FSM_AI_Near>().player;

    }
    

}