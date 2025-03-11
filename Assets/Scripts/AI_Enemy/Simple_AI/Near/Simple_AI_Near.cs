using Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Simple_AI_Near : MonoBehaviour
{

    [Header("Combat")]
    [SerializeField] float Damage = 1f;
    [SerializeField] float attackRange = 1.2f;
    [SerializeField] float aggroRange = 5f;

    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

        if(Vector3.Distance(player.transform.position, transform.position) <= attackRange){
            transform.LookAt(player.transform);
            animator.SetTrigger("attack");
        }
        else if (Vector3.Distance(player.transform.position, transform.position) <= aggroRange){
            transform.LookAt(player.transform);
            agent.SetDestination(player.transform.position);
        }

    }

    public void StartDealDamage(){
        player.GetComponent<Animator>().SetTrigger("damage");
        player.GetComponent<Actor>().ApplyDamage(Damage);
    }

    private void OnDrawGizmos() {
        Gizmos.color =Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
