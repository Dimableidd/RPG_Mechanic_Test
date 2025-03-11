using Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Simple_AI_Far : MonoBehaviour
{

    [Header("Combat")]
    [SerializeField] float Damage = 2f;
    [SerializeField] float attackRange = 6f;
    [SerializeField] float aggroRange = 10f;

    [Header("Prafab")]
    [SerializeField] GameObject Pistol;

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
            animator.SetFloat("speed", 0);
            animator.SetTrigger("attack");
        }

        if (Vector3.Distance(player.transform.position, transform.position) <= aggroRange){
            transform.LookAt(player.transform);
            agent.SetDestination(player.transform.position);
        }
    }

    public void StartDealDamage(){
        Pistol.GetComponent<Pistol>().Init(Damage, player);
    }

    private void OnDrawGizmos() {
        Gizmos.color =Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
