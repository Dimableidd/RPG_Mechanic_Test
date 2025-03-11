using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Entities;
using Logics.Health;
using BehaviourTree;

public class TasksHealthAny : Node
{
    private GameObject _colliderObj;
    private Animator _animator;
    private NavMeshAgent _agent;
    private Transform _transform;
    private GameObject _healthObj;
    private float _healthCount, _cooldownSpell, cooldown;
    private List<GameObject> collidedObjects = new List<GameObject>();
    
    public TasksHealthAny(NavMeshAgent agent, Transform transform, GameObject colliderObj, GameObject healthObj, float healthCount, float cooldownSpell)
    {
        _colliderObj = colliderObj;
        _agent = agent;
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _healthObj = healthObj;
        _healthCount = healthCount;
        _cooldownSpell = cooldownSpell;
        cooldown = _cooldownSpell;
    }
    public override NodeState Evaluate()
    {
        collidedObjects = _colliderObj.GetComponent<Colliders>().Init();
        if (collidedObjects.Count > 0)
        {
            foreach(GameObject obj in collidedObjects)
            {
                if (obj == null) 
                {
                    state = NodeState.FAILURE;
                    return state;
                }
                if (obj.GetComponent<CHealth>().CurrentHealth < obj.GetComponent<CHealth>().MaxHealth / 2f)
                {
                    if(cooldown >= _cooldownSpell)
                    {
                        _agent.isStopped = true;
                        _transform.LookAt(obj.transform);
                        _animator.SetTrigger("health_any");
                        var temp = GameObject.Instantiate(_healthObj, obj.transform.position,  Quaternion.Euler(-90, 0, 0)).GetComponent<Healt_seal>();
                        temp.Init(_healthCount);
                        cooldown = 0f;
                        state = NodeState.SUCCESS;
                        return state;
                    }
                    cooldown += Time.deltaTime;
                }
            }
        }
        
        state = NodeState.FAILURE;
        return state;
    }

}