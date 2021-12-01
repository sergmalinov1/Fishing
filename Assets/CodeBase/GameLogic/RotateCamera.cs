using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.BobberObject.DisplayCatchedFish
{
  public class RotateCamera : MonoBehaviour
  {

    private int _angleRotated = 25;
    
    public void RotateCameraUp()
    {
      StartCoroutine(RotateUp());
    }

    public void RotateCameraDown()
    {
      StartCoroutine(RotateDown());
    }

    private IEnumerator RotateUp()
    {
      Quaternion rorationY;
      for (int i = 0; i < _angleRotated; i++)
      {
        rorationY = Quaternion.AngleAxis(1, Vector3.left);
        transform.rotation *= rorationY;
        yield return new WaitForSeconds(.02f);
      }
    }
    
    private IEnumerator RotateDown()
    {
      Quaternion rorationY;
      for (int i = 0; i < _angleRotated; i++)
      {
        rorationY = Quaternion.AngleAxis(-1, Vector3.left);
        transform.rotation *= rorationY;
        yield return new WaitForSeconds(.02f);
      }
    }
  }
}