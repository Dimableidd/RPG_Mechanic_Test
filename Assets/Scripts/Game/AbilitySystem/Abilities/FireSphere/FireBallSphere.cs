using Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSphere : MonoBehaviour
{
    [field: SerializeField] public GameObject Explosions { get; private set; }
    private float p_damage, p_duration, p_curDuration = 0f;
    private Vector3 p_spawnPoint;
    private Vector3 p_destinationPoint;

    public void Init(float _damage, float _duration, Vector3 _spawnPoint, Vector3 _destinationPoint)
    {
        p_damage = _damage;
        p_duration = _duration;
        p_spawnPoint = _spawnPoint;
        p_destinationPoint = _destinationPoint;
    }

    private void Start()
    {
        Vector3 Difference = new Vector3(p_destinationPoint.x - transform.position.x, 0f, p_destinationPoint.z - transform.position.z);
        transform.rotation = Quaternion.LookRotation(-(Difference));
    }

    private void Update() {

        transform.position += new Vector3((p_destinationPoint.x - p_spawnPoint.x) * Time.deltaTime, 
        0f, (p_destinationPoint.z - p_spawnPoint.z) * Time.deltaTime);

        if(p_curDuration > p_duration)
        {
            GameObject.Instantiate(Explosions, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        p_curDuration += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Animator>().SetTrigger("damage_spell");
            GameObject.Instantiate(Explosions, transform.position, Quaternion.identity);
            other.GetComponent<Actor>().ApplyDamage(p_damage);
            Destroy(gameObject);
        }
    }
}
