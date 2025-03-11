using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    [SerializeField] public GameObject Seal;
    private float p_duration = 3, p_curDuration = 0, _Damage;
    public void Init(float Damage)
    {
        _Damage = Damage;
    }
    void Update()
    {
        if(p_curDuration >= p_duration)
        {
            var temp = GameObject.Instantiate(Seal, transform.position, Quaternion.Euler(-90, 0, 0)).GetComponent<Seal>();
            temp.Init(_Damage);
            Destroy(gameObject);
        }
        p_curDuration += Time.deltaTime;
    }
}
