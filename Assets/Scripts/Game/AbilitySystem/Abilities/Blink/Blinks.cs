using Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinks : MonoBehaviour
{
    private Actor _Owner;
    private Vector3 p_destinationPoint;
    private float dur = 3;
    private float cdur = 0;

    public void Init(Actor owner, Vector3 _destinationPoint)
    {
        _Owner = owner;
        p_destinationPoint = _destinationPoint;
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

        _Owner.SetLocation(p_destinationPoint);
    }

    private void Update() {

        if(cdur >= dur)
        {
            Destroy(gameObject);
        }
        cdur += Time.deltaTime;
    }

}