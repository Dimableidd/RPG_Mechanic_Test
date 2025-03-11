using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [Header("Prafab")]
    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject Flash;
    [SerializeField] GameObject Hit;

    public void Init(float Damage, GameObject player)
    {
        Vector3 _Des = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);
        Vector3 Difference = new Vector3(_Des.x - transform.position.x, 0f, _Des.z - transform.position.z);
        GameObject.Instantiate(Flash, transform.position, Quaternion.LookRotation((Difference)));
        var temp = GameObject.Instantiate(Bullet, transform.position, Quaternion.LookRotation(-(Difference))).GetComponent<Fire_Bullet>();
        temp.Init("Player", Damage, transform.position, _Des, Hit);
    }

}
