using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

public class CheckAttacking : Node
{
    private GameObject _player;
    private GameObject _autoSpell;
    private ParticleSystem  _autoSpell_1;
    private ParticleSystem  _autoSpell_2;
    private Animator _animator;
    private NavMeshAgent _agent;
    private Transform _transform;
    private float _attackRange, dur = 5f;

    public CheckAttacking(NavMeshAgent agent, Transform transform, float attackRange, GameObject autoSpell)
    {
        _player = GameObject.FindWithTag("Player");
        _autoSpell = autoSpell;
        _autoSpell_1 = autoSpell.transform.Find("Soul_1").gameObject.GetComponent<ParticleSystem>();
        _autoSpell_2 = autoSpell.transform.Find("Soul_2").gameObject.GetComponent<ParticleSystem>();
        _agent = agent;
        _animator = transform.GetComponent<Animator>();
        _transform = transform;
        _attackRange = attackRange;
    }

    public override NodeState Evaluate()
    {
        var emission_1 = _autoSpell_1.emission;
        var emission_2 = _autoSpell_2.emission;
        if (Vector3.Distance(_player.transform.position, _transform.position) <= _attackRange){
            _autoSpell.SetActive(true);
            emission_1.rateOverTimeMultiplier = 20f;
            emission_2.rateOverTimeMultiplier = 10f;
            if(_autoSpell_1.isStopped && _autoSpell_2.isStopped)
            {
                _autoSpell_1.Play();
                _autoSpell_2.Play();
            }
            state = NodeState.SUCCESS;
            return state;
        }
        else
        {
            if (_autoSpell_1.isPlaying && _autoSpell_2.isPlaying)
            { 
                emission_1.rateOverTimeMultiplier -= Time.deltaTime * 5f;
                emission_2.rateOverTimeMultiplier -= Time.deltaTime * 5f;
                if (emission_1.rateOverTimeMultiplier <= 0f && emission_2.rateOverTimeMultiplier <= 0f)
                {
                    _autoSpell_1.Stop();
                    _autoSpell_2.Stop();
                    dur -= Time.deltaTime;
                    if(dur <= 0)
                    {
                        emission_1.rateOverTimeMultiplier = 20f;
                        emission_2.rateOverTimeMultiplier = 10f;
                        _autoSpell.SetActive(false);
                    }
                }
            }
            _agent.isStopped = false;
            _animator.SetBool("isAuto", false);
            state = NodeState.FAILURE;
            return state;
        }
    }


}