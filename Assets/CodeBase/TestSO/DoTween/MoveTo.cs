using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MoveTo : MonoBehaviour
{

    void Start()
    {
        transform.DOMove(new Vector3(0, 5, 0), 3).SetDelay(2);
    }

  
}
