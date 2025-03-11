using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

public class TaskAutoAttack : Node
{
    private GameObject _player;
    private Transform _transform;
    private NavMeshAgent _agent;
    private Animator _animator;

    public TaskAutoAttack(NavMeshAgent agent, Transform transform)
    {
        _player = GameObject.FindWithTag("Player");
        _transform = transform;
        _agent = agent;
        _animator = agent.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        _agent.isStopped = true;
        _transform.LookAt(_player.transform);
        _animator.SetBool("isAuto", true);

        state = NodeState.RUNNING;
        return state;
    }

}