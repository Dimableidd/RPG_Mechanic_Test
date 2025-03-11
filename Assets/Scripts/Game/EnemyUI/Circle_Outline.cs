using System.Collections;
using System.Collections.Generic;
using Game.Entities;
using Game.EnemyUI;
using UnityEngine;
using UnityEngine.UI;

public class Circle_Outline : MonoBehaviour
{
    [SerializeField] private GameObject _circle;
    private GameObject _healthBar;
    private ParticleSystem particleSystem;

    private int actorLayerMask;
    private int groundLayerMask;
    private int noCollisionLayerMask;
    private int finalLayerMask;

    public void Awake()
    {
        actorLayerMask = LayerMask.GetMask("Actors");
        noCollisionLayerMask = LayerMask.GetMask("Interactable");
        finalLayerMask = actorLayerMask & ~noCollisionLayerMask;
        particleSystem = _circle.GetComponent<ParticleSystem>();
        particleSystem.Stop();
        _healthBar = GameObject.FindWithTag("Health_Bar");
    }

    private void Update() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, finalLayerMask))
        {
            Collider hitCollider = hit.collider;
            
            if (hitCollider == GetComponent<Collider>())
            {
                if (particleSystem != null && _healthBar != null && particleSystem.isStopped) 
                {
                    particleSystem.Play();
                    _healthBar.SetActive(true);
                    _healthBar.GetComponent<UIHealth_Bar>().Show(gameObject.GetComponent<Actor>());
                }
            }
        }
        else if (particleSystem != null && _healthBar != null && particleSystem.isPlaying)
            {
                particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                _healthBar.SetActive(false);
            }
    }

}
