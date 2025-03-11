using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TriggerOn : MonoBehaviour
{
    private GameObject _autoSpell;
    private ParticleSystem  _autoSpell_1;
    private ParticleSystem  _autoSpell_2;
    private float dur = 5f;
    void Start()
    {
        _autoSpell = gameObject;
    }

    void Update()
    {
        _autoSpell_1 = transform.Find("Soul_1").gameObject.GetComponent<ParticleSystem>();
        _autoSpell_2 = transform.Find("Soul_2").gameObject.GetComponent<ParticleSystem>();
        if (_autoSpell_1.isPlaying && _autoSpell_2.isPlaying)
        { 
            var emission_1 = _autoSpell_1.emission;
            var emission_2 = _autoSpell_2.emission;
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
    }
}
