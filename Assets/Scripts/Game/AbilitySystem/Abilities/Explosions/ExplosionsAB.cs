using Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionsAB : MonoBehaviour
{
    private float p_damage;
    private Actor _target;
    private float dur = 2;
    private float cdur = 0;

    public void Init(float _damage, Actor target)
    {
        p_damage = _damage;
        _target = target;
    }

    private void Start()
    {
        _target.GetComponent<Animator>().SetTrigger("damage");
        _target.ApplyDamage(p_damage);
    }

    private void Update() {

        if(cdur >= dur)
        {
            Destroy(gameObject);
        }
        cdur += Time.deltaTime;
    }
}
