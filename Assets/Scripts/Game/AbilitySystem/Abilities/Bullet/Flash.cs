using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    private float p_duration = 1, p_curDuration = 0;
    void Update()
    {
        if(p_curDuration >= p_duration)
        {
            Destroy(gameObject);
        }
        p_curDuration += Time.deltaTime;
    }
}
