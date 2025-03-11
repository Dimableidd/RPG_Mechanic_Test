using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colliders : MonoBehaviour
{
    public List<GameObject> collidedObjects = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (!collidedObjects.Contains(other.gameObject) && other.CompareTag("Enemy"))
        {
            collidedObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (collidedObjects.Contains(other.gameObject))
        {
            collidedObjects.Remove(other.gameObject);
        }
    }

    public List<GameObject> Init()
    {
        return collidedObjects;
    }
}
