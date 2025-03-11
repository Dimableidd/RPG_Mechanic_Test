using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Shot : MonoBehaviour
{   
    private float Damage;
    private Vector3 _Des;
    private GameObject Flash, Hit, Bullet;
    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _time = false;
    public void ShotTime (bool Time, Animator animator)
    {
        _animator = animator;
        _time = Time;
    }
    public void Init(float damage, Vector3 _des, GameObject flash, GameObject hit, GameObject bullet)
    {
        Damage = damage;
        _Des = _des;
        Flash = flash;
        Hit = hit;
        Bullet = bullet;
        _agent = transform.GetComponent<NavMeshAgent>();
    }

    public void StartDealDamage(){
        _agent.isStopped = true;
        _agent.ResetPath();
        GameObject Pistol = GameObject.FindWithTag("Pistol_GG");    
        Pistol.GetComponent<Pistol_GG>().Init(Damage, _Des, Flash, Hit, Bullet, _time, _animator);
    }
}
