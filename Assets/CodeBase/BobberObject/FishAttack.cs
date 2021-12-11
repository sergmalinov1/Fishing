using System;
using CodeBase.StaticData.Fish;
using UnityEngine;

namespace CodeBase.BobberObject
{
  public class FishAttack : MonoBehaviour
  {

    private float _timeToEndAttack = 5.0f;
    private bool _isAttack = false;
    public event Action FishEndAttack;

    private void Update()
    {
      if (_isAttack)
      {
        _timeToEndAttack -= Time.deltaTime;
        if (_timeToEndAttack <= 0.0f)
        {
          FishEndAttack?.Invoke();
          _isAttack = false;
        }
      }
    }
    
  }
}