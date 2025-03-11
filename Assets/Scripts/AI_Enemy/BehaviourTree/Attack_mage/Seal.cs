using System.Collections;
using System.Collections.Generic;
using Game.Entities;
using UnityEngine;

public class Seal : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private float p_duration = 6, p_curDuration = 0, _Damage;
    private GameObject _player;
    private Collider collider;
    public void Init(float Damage)
    {
        _Damage = Damage;
    }
    private void Start() {
        particleSystem = GetComponent<ParticleSystem>();
        _player = GameObject.FindWithTag("Player");
        collider = _player.GetComponent<CapsuleCollider>();


        var triggerModule = particleSystem.trigger;
        triggerModule.SetCollider(0, collider);

    }
    private void Update() {
        if(p_curDuration >= p_duration)
        {
            Destroy(gameObject);
        }
        p_curDuration += Time.deltaTime;
    }
    private void OnParticleTrigger()
    {
        _player.GetComponent<Actor>().ApplyDamage(_Damage);
    }
}
