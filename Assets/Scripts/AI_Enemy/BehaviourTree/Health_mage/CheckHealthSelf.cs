using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Logics.Health;
using BehaviourTree;

public class CheckHealthSelf : Node
{
    private NavMeshAgent _agent;
    private Transform _transform;
    public CheckHealthSelf(NavMeshAgent agent, Transform transform)
    {
        _agent = agent;
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        if (_transform.GetComponent<CHealth>().CurrentHealth < _transform.GetComponent<CHealth>().MaxHealth / 2f)
        {
            state = NodeState.SUCCESS;
            return state;
        }
        _agent.isStopped = false;
        state = NodeState.FAILURE;
        return state;
        
    }


}