using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol_GG : MonoBehaviour
{
    private Animator _animator;
    private bool _time = false;
    private float onemouseTimer = 0f;
    public void Init(float Damage, Vector3 _Des, GameObject Flash, GameObject Hit, GameObject Bullet, bool Time, Animator animator)
    {
        _animator = animator;
        _time = Time;
        Vector3 Difference = new Vector3(_Des.x - transform.position.x, 0f, _Des.z - transform.position.z);
        GameObject.Instantiate(Flash, transform.position, Quaternion.LookRotation((Difference)));
        var temp = GameObject.Instantiate(Bullet, transform.position, Quaternion.LookRotation(-(Difference))).GetComponent<Fire_Bullet>();
        temp.Init("Enemy", Damage, transform.position, _Des, Hit);
    }

    private void Update() {
        if(_time) onemouseTimer += Time.deltaTime;

        if (onemouseTimer > 0.2f)
            {
                _animator.SetBool("Move", true);
                onemouseTimer = 0f;
                _time = false;
            }
    }
}
