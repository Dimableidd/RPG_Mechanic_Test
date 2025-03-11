using System.Collections;
using System.Collections.Generic;
using Game.Entities;
using Logics.Health;
using UnityEngine;

public class Healt_seal : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private float p_duration = 6, p_curDuration = 0, _healthCount;
    public List<GameObject> collidedObjects = new List<GameObject>();
    public List<Collider> colliders = new List<Collider>();
    private Collider collider;
    public void Init(float healthCount)
    {
        _healthCount = healthCount;
    }
    private void Start() 
    {
        particleSystem = GetComponent<ParticleSystem>();
    }
    private void Update() {
        collidedObjects = GetComponent<Colliders>().Init();
        if(collidedObjects.Count > 0){
            var triggerModule = particleSystem.trigger;
            foreach(GameObject obj in collidedObjects)
            {
                colliders.Add(obj.GetComponent<CapsuleCollider>());
            }
            int i = 0;
            foreach(Collider coll in colliders)
            {
                triggerModule.SetCollider(i++, coll);
            }
        }

        if(p_curDuration >= p_duration)
        {
            Destroy(gameObject);
        }
        p_curDuration += Time.deltaTime;
    }
    private void OnParticleTrigger()
    {
        foreach(GameObject obj in collidedObjects)
        {
            obj.GetComponent<CHealth>().AddHealth(_healthCount);
        }

    }
}