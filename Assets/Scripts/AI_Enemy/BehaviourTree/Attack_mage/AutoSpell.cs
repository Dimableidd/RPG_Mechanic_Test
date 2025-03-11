using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpell : MonoBehaviour
{
    public void Init(float Damage, GameObject player, GameObject Flash, GameObject Spell_Bullet)
    {
        Vector3 _Des = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);
        Vector3 Difference = new Vector3(_Des.x - transform.position.x, 1f, _Des.z - transform.position.z);
        var temp = GameObject.Instantiate(Spell_Bullet, transform.position, Quaternion.LookRotation((-Difference)) * Quaternion.Euler(0, 90, 0)).GetComponent<Spell_Bullet>();
        temp.Init("Player", Damage, transform.position, _Des, Flash);
    }
}
