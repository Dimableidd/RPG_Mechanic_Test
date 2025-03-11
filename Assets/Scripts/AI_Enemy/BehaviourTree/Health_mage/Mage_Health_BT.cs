using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

public class Mage_Health_BT : Trees
{
    [SerializeField] protected Transform transform;
    [SerializeField] protected Transform pointsObject;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected float Damage_auto = 0.02f;
    [SerializeField] protected float aggroRange = 10f;
    [SerializeField] protected float attackRange = 6f;
    [SerializeField] protected float healthCountSelf = 20f;
    [SerializeField] protected float healthCountAny = 0.02f;
    [SerializeField] protected float cooldownHealthSelf = 10f;
    [SerializeField] protected float cooldownHealthAny = 20f;
    [SerializeField] protected GameObject autoSpell;
    [SerializeField] protected GameObject health;
    [SerializeField] protected GameObject _colliderObj;
    [SerializeField] protected GameObject healthObj;


    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Selector(new List<Node>{
                new Sequence(new List<Node>{
                    new CheckHealthSelf(agent, transform),
                    new TasksHealthSelf(agent, transform, transform.gameObject, health, healthCountSelf, cooldownHealthSelf),
                }),
                new TasksHealthAny(agent, transform, _colliderObj, healthObj, healthCountAny, cooldownHealthAny)
            }),
            new Sequence(new List<Node>
            {
                new CheckAttacking(agent, transform, attackRange, autoSpell),
                new TasksAutoAttacking(agent, transform, Damage_auto),
            }),
            new Sequence(new List<Node>
            {
                new CheckChasing(transform, aggroRange),
                new TasksChasing(agent, transform),
            }),
            new TasksPatrolling(agent, pointsObject),
        });
        
        return root;
    }

    private void OnDrawGizmos() {
        Gizmos.color =Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
    
}