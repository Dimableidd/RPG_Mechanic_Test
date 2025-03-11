using Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSM_Base_Far : StateMachineBehaviour
{
    protected float Damage = 2f;
    protected float attackCD = 2f;
    protected float attackRange = 6f;
    protected float aggroRange = 10f;

    protected GameObject player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<FSM_AI_Far>().player;

    }
    

}