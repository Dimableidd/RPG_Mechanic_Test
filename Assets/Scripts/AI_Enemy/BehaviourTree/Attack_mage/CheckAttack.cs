using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

public class CheckAttack : Node
{
    private GameObject _player;
    private Animator _animator;
    private NavMeshAgent _agent;
    private Transform _transform;
    private float _attackRange;

    public CheckAttack(NavMeshAgent agent, Transform transform, float attackRange)
    {
        _player = GameObject.FindWithTag("Player");
        _agent = agent;
        _animator = transform.GetComponent<Animator>();
        _transform = transform;
        _attackRange = attackRange;
    }

    public override NodeState Evaluate()
    {
        if (Vector3.Distance(_player.transform.position, _transform.position) <= _attackRange){
            state = NodeState.SUCCESS;
            return state;
        }
        else
        {
            _agent.isStopped = false;
            _animator.SetBool("isAuto", false);
            state = NodeState.FAILURE;
            return state;
        }
    }

}