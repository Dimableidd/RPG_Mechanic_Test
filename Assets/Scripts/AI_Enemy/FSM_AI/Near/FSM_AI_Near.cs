using Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_AI_Near : MonoBehaviour
{
    public GameObject player;

    protected float Damage = 1f;
    protected float attackCD = 1f;
    protected float attackRange = 1.5f;
    protected float aggroRange = 5f;

    private void Start() {
        player = GameObject.FindWithTag("Player");
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

    public void StartDealDamage(){
        player.GetComponent<Actor>().ApplyDamage(Damage);
    }
}