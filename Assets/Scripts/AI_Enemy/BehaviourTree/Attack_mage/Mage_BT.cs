using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

public class Mage_BT : Trees
{
    [SerializeField] protected Transform transform;
    [SerializeField] protected Transform pointsObject;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected float Damage_auto = 2f;
    [SerializeField] protected float Damage_spell = 0.1f;
    [SerializeField] protected float aggroRange = 10f;
    [SerializeField] protected float attackRange = 6f;
    [SerializeField] protected float cooldownSpell = 12f;
    [SerializeField] protected GameObject autoSpell;
    [SerializeField] protected GameObject Flash;
    [SerializeField] protected GameObject Spell_Bullet;
    [SerializeField] protected GameObject Sign;
    

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckAttack(agent, transform, attackRange),
                    new Selector(new List<Node>
                    {
                        new TaskSpellAttack(Sign, Damage_spell, agent, transform, cooldownSpell),
                        new TaskAutoAttack(agent, transform),
                    }),
            }),
            new Sequence(new List<Node>
            {
                new CheckChase(transform, aggroRange),
                new TaskChase(agent, transform),
            }),
            new TaskPatrol(agent, pointsObject),
        });
        
        return root;
    }

    public void AutoSpell(){
        autoSpell.GetComponent<AutoSpell>().Init(Damage_auto, GameObject.FindWithTag("Player"), Flash, Spell_Bullet);
    }

    private void OnDrawGizmos() {
        Gizmos.color =Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
    
}