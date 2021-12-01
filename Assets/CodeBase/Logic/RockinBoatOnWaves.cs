using System;
using UnityEngine;

namespace CodeBase.Logic
{
  public class RockinBoatOnWaves : MonoBehaviour
  {
    
    private bool isLeftRocking = true;
    private int _maxCountRotation = 150;
    private int _nowCountRotation = 75;
    private float waitTime = 0.03f;
    private float timer = 0.0f;
    

    private void Update()
    {
      timer += Time.deltaTime;
      
      if(timer < waitTime)
        return;

      timer = 0.0f;
      
      Quaternion rorationY;

      if (_maxCountRotation == _nowCountRotation)
      {
        _nowCountRotation = 0;
        isLeftRocking = !isLeftRocking;
      }
      
      
      if (isLeftRocking)
      {
        rorationY = Quaternion.AngleAxis(0.1f, Vector3.back); //вращение по часововй
      }
      else
      {
        rorationY = Quaternion.AngleAxis(-0.1f, Vector3.back); //вращение против часововй
      }
      
      _nowCountRotation++;
      transform.rotation *= rorationY;
    }
  }
}