using Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorAb : MonoBehaviour
{
    private float p_damage;
    private Vector3 p_spawnPoint;
    private Vector3 p_destinationPoint;
    private float dur = 3;
    private float cdur = 0;

    public void Init(float _damage, Vector3 _spawnPoint, Vector3 _destinationPoint)
    {
        p_damage = _damage;
        p_spawnPoint = _spawnPoint;
        p_destinationPoint = _destinationPoint;
    }

    private void Start()
    {
        Vector3 Difference = new Vector3(p_destinationPoint.x - transform.position.x, 0f, p_destinationPoint.z - transform.position.z);
        Quaternion CurrentQuaternion = Quaternion.LookRotation(-(Difference));
        Quaternion NewQuaternion = Quaternion.Euler(-90f, 1f, 1f);
        transform.rotation = Quaternion.Euler(-90f, CurrentQuaternion.eulerAngles.y, 0f);
    }

    private void Update() {

        if(cdur >= dur)
        {
            Destroy(gameObject);
        }
        cdur += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Animator>().SetTrigger("damage_spell");
            other.GetComponent<Actor>().ApplyDamage(p_damage);
        }
    }
}