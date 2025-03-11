using Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink_health : MonoBehaviour
{
    private Vector3 p_destinationPoint;
    private float dur = 3;
    private float cdur = 0;

    public void Init(Vector3 _destinationPoint)
    {
        p_destinationPoint = _destinationPoint;
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }

    private void Update() {

        if(cdur >= dur)
        {
            Destroy(gameObject);
        }
        cdur += Time.deltaTime;
    }

}