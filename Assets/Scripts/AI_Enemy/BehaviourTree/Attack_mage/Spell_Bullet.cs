using Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Bullet : MonoBehaviour
{
    private string p_tag;
    private float p_damage, p_duration = 0.5f, p_curDuration;
    private Vector3 p_spawnPoint;
    private Vector3 p_destinationPoint;
    private GameObject Flash;

    public void Init(string tag, float _damage, Vector3 _spawnPoint, Vector3 _destinationPoint, GameObject flash)
    {
        p_tag = tag;
        p_damage = _damage;
        p_spawnPoint = _spawnPoint;
        p_destinationPoint = _destinationPoint;
        Flash = flash;
    }

    private void Update() {

        transform.position += new Vector3((p_destinationPoint.x - p_spawnPoint.x) * Time.deltaTime* 3f, 
        0f, (p_destinationPoint.z - p_spawnPoint.z) * Time.deltaTime * 3f);

        if(p_curDuration >= p_duration)
        {
            Destroy(gameObject);
        }
        p_curDuration += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == p_tag)
        {
            GameObject.Instantiate(Flash, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 0.3f), Quaternion.identity);
            other.GetComponent<Actor>().ApplyDamage(p_damage);
            Destroy(gameObject);
        }
    }
    
}
