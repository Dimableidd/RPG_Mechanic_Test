using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

public class TasksPatrolling : Node
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private Transform _pointsObject;
    private List<Transform> points = new List<Transform>();
    private int _currentWaypointIndex = 0;
    private int _randomWaypointIndex = 0;

    private float _waitTime = 5f;
    private float _waitCounter = 0f;
    private bool _waiting = false;
    private bool _first = true;

    public TasksPatrolling(NavMeshAgent agent, Transform pointsObject)
    {
        _agent = agent;
        _animator = agent.GetComponent<Animator>();
        _pointsObject = pointsObject;
    }

    public override NodeState Evaluate()
    {
        if (_first)
        {
            foreach (Transform t in _pointsObject) 
                points.Add(t);
            _animator.SetBool("isPatrolling", true);
            _agent.SetDestination(points[_currentWaypointIndex = Random.Range(0, points.Count)].position);
            _first = false;
        }
        else
        {
            if (_waiting)
            {
                _waitCounter += Time.deltaTime;
                if (_waitCounter >= _waitTime)
                {
                    _animator.SetBool("isPatrolling", true);
                    bool stateWay = true;
                    while (stateWay)
                    {
                        _randomWaypointIndex = Random.Range(0, points.Count);
                        if (_currentWaypointIndex != _randomWaypointIndex)
                        {
                            _agent.SetDestination(points[_currentWaypointIndex = _randomWaypointIndex].position);
                            stateWay = false;
                        }
                    }
                    _waiting = false;
                }
            }
            else
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    _animator.SetBool("isPatrolling", false);
                    _waitCounter = 0f;
                    _waiting = true;
                }
            
            }

        }
    
        state = NodeState.RUNNING;
        return state;
    }

}

