using System;
using CodeBase.Data;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData.Fish;
using LostPolygon.DynamicWaterSystem;
using UnityEngine;

namespace CodeBase.BobberObject
{
  public class Bobber : MonoBehaviour
  {
    private Small_Splash _splash;
    
    private void Awake()
    {
      _splash = GetComponentInChildren<Small_Splash>();

    }

    private void OnTriggerEnter(Collider other)
    {
      if (other.tag == "DynamicWater")
      {
        Debug.Log("DynamicWater");
        _splash.ShowSplash();
      }
    }


    
  }
}