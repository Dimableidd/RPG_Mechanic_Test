using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Entities;
using Logics.Health;
using BehaviourTree;

public class TasksHealthSelf : Node
{
    private GameObject _target;
    private Animator _animator;
    private NavMeshAgent _agent;
    private Transform _transform;
    private GameObject _health;
    private float _healthCount, _cooldownSpell, cooldown;
    private Vector3 _Des;
    
    public TasksHealthSelf(NavMeshAgent agent, Transform transform, GameObject target, GameObject health, float healthCount, float cooldownSpell)
    {
        _target = target;
        _agent = agent;
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _health = health;
        _healthCount = healthCount;
        _cooldownSpell = cooldownSpell;
        cooldown = _cooldownSpell;
    }

    public override NodeState Evaluate()
    {
        if(cooldown >= _cooldownSpell)
        {
            _animator.SetTrigger("health_self");
            _agent.isStopped = true;
            _Des = new Vector3 (_target.transform.position.x, _target.transform.position.y + 3f, _target.transform.position.z);
            var temp = GameObject.Instantiate(_health, _Des, Quaternion.identity).GetComponent<Blink_health>();
            temp.Init(_Des);
            _target.GetComponent<CHealth>().AddHealth(_healthCount);
            cooldown = 0f;

            state = NodeState.SUCCESS;
            return state;
        }
        else
        {
            cooldown += Time.deltaTime;
            state = NodeState.FAILURE;
            return state;
        }
    }

}