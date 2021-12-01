using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineChance : MonoBehaviour
{
    public float Chanse = 0;
    private float _lastUpdateTime = 0f;


    void Update()
    {
        if (Time.time - _lastUpdateTime >= 1.0f)
        {
            Debug.Log(Chanse);
            _lastUpdateTime = Time.time;

        }
    }
}
