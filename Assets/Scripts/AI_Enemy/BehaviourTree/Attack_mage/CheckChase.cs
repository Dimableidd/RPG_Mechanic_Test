using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

public class CheckChase : Node
{
    private GameObject _player;
    private Animator _animator;
    private Transform _transform;
    private float _aggroRange;

    public CheckChase(Transform transform, float aggroRange)
    {
        _player = GameObject.FindWithTag("Player");
        _animator = transform.GetComponent<Animator>();
        _transform = transform;
        _aggroRange = aggroRange;
    }

    public override NodeState Evaluate()
    {
        if (Vector3.Distance(_player.transform.position, _transform.position) <= _aggroRange){
            state = NodeState.SUCCESS;
            return state;
        }
        else
        {
            _animator.SetBool("isChasing", false);
            state = NodeState.FAILURE;
            return state;
        }
    }

}
