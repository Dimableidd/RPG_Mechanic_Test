using Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_AI_Far : MonoBehaviour
{
    public GameObject player;

    protected float Damage = 2f;
    protected float attackRange = 6f;
    protected float aggroRange = 10f;

    private Vector3 _Des = Vector3.zero;
    private Vector3 _Pos = Vector3.zero;

    [Header("Prafab")]
    [SerializeField] GameObject Pistol;

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
        Pistol.GetComponent<Pistol>().Init(Damage, player);
    }
}