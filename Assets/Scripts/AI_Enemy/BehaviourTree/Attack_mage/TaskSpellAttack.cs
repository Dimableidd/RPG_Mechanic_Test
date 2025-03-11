using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

public class TaskSpellAttack : Node
{
    private GameObject _player;
    private GameObject _Sign;
    private Transform _transform;
    private NavMeshAgent _agent;
    private Animator _animator;
    private float _Damage;
    private float _cooldownSpell, cooldown;

    public TaskSpellAttack(GameObject Sign, float Damage, NavMeshAgent agent, Transform transform, float cooldownSpell)
    {
        _player = GameObject.FindWithTag("Player");
        _Sign = Sign;
        _Damage = Damage;
        _transform = transform;
        _agent = agent;
        _animator = agent.GetComponent<Animator>();
        _cooldownSpell = cooldownSpell;
        cooldown = _cooldownSpell;
    }

    public override NodeState Evaluate()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            _agent.isStopped = true;
            _transform.LookAt(_player.transform);
            _animator.SetTrigger("isSpell");
            var temp = GameObject.Instantiate(_Sign, _player.transform.position, Quaternion.Euler(-90, 0, 0)).GetComponent<Sign>();
            temp.Init(_Damage);
            cooldown = _cooldownSpell;
            state = NodeState.SUCCESS;
            return state;
        }
        else
        {
            state = NodeState.FAILURE;
            return state;
        }
    }

}