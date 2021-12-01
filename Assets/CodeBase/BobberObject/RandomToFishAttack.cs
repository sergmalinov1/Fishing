using System;
using CodeBase.StaticData.Fish;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.BobberObject
{
  public class RandomToFishAttack : MonoBehaviour
  {

    private float _coutDown = 0.0f;

    private bool _isStartAttack = false;

    public event Action FishBite;

    public void StartRandomAttack()
    {
      _coutDown = Random.Range(7.0f, 10.0f);
      _isStartAttack = true;
    }
    
    public void StopAttack()
    {
      _isStartAttack = false;
    }

    private void Update()
    {
      if (_isStartAttack)
      {
        _coutDown -= Time.deltaTime;
        if (_coutDown <= 0.0f)
        {
          FishBite?.Invoke();
          _isStartAttack = false;
        }
      }
    }
  }
}