using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Entities;
using BehaviourTree;

public class TasksAutoAttacking : Node
{
    private GameObject _player;
    private Transform _transform;
    private NavMeshAgent _agent;
    private Animator _animator;
    private float _damage;
    public TasksAutoAttacking(NavMeshAgent agent, Transform transform, float Damage_auto)
    {
        _player = GameObject.FindWithTag("Player");
        _transform = transform;
        _agent = agent;
        _animator = agent.GetComponent<Animator>();
        _damage = Damage_auto;
    }

    public override NodeState Evaluate()
    {
        _player.GetComponent<Actor>().ApplyDamage(_damage);
        _agent.isStopped = true;
        _transform.LookAt(_player.transform);
        _animator.SetBool("isAuto", true);

        state = NodeState.RUNNING;
        return state;
    }

}