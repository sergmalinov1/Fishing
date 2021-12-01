using System;
using System.Collections;
using CodeBase.BobberObject;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using CodeBase.StaticData.Fish;
using UnityEngine;

namespace CodeBase.BobberLogic
{
  
  public enum BobberState
  {
    InAir = 0,
    InWater,
    Pull,
  }
  
  public class BobberSpawner : MonoBehaviour
  {
    private Camera _camera;
    private IGameFactory _gameFactory;
    private IInputService _input;
    private PlayerProgress _playerProgress;
    private IStaticDataService _staticData;
    private ISaveLoadService _saveLoadService;

    private BobberState _state = BobberState.InAir;

    private GameObject bobberObj;
    private Bobber _bobber;

    private bool fishOnHook = false;


    private void Start()
    {
      
      _camera = Camera.main;

      if (_camera == null)
      {
        Debug.Log("Camera not find");
      }
        
        
      _input = AllServices.Container.Single<IInputService>();
      _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
    }

    public void Construct(IGameFactory gameFactory, PlayerProgress playerProgress, IStaticDataService staticData)
    {
     _gameFactory = gameFactory;
     _playerProgress = playerProgress;
     _staticData = staticData;

     _playerProgress.MoneyData.Changed += FishSold;
    }

    private void Update()
    {
      if (_input.IsAttackButtonUp())
      {
        if (_state == BobberState.InAir)
        {
          CreateBobber();
          _state = BobberState.InWater;

        }
        
        if (_state == BobberState.InWater)
        {
          if (_bobber != null)
          {
            EndAttack();
          }
        }
      }
    }

    private void CheckResult()
    {
      if (fishOnHook)
      {
        //Рандомайзер для определения типа пойманной рыбы
       // FishTypeId typeId = FishTypeId.Sardine;

       // FishStaticData fishData = _staticData.ForFish(typeId);
        _playerProgress.FishOnHook.CatchFish();
        
       // _bobber.FishObject.SetActive(true);
        StartCoroutine(BobberMoveUp());

        fishOnHook = false;
        
      }
      else
      {
        _playerProgress.FishOnHook.NotCatchFish();
          // _bobber.HookObject.SetActive(true);
        StartCoroutine(BobberMoveUp());
      }
    }

    private void FishSold()
    {
      Destroy(bobberObj);
      
    }

    private async void CreateBobber()
    {
      float distanceFromCamera = 13.0f;
      float colibtateAngle = 4.0f; //Выравниваю поплавок по центру экрана

      float angle = _camera.transform.rotation.eulerAngles.y - colibtateAngle;

      
      GameObject tempObj = new GameObject();
      
      tempObj.transform.position = new Vector3(_camera.transform.position.x , 8.0f, _camera.transform.position.z + distanceFromCamera);
      tempObj.transform.RotateAround(_camera.transform.position, Vector3.up, angle);
      
   //   Vector3 target = new Vector3(_camera.transform.position.x , 10.0f, _camera.transform.position.z + distanceFromCamera);
      
      
      bobberObj = await _gameFactory.CreateBobber(tempObj.transform.position);
  
      _bobber =  bobberObj.GetComponent<Bobber>();
      
      
   //   _bobber.Construct(_input, _playerProgress);
   //   _bobber.Initialize();
      
   //   FishAttack fishAttack =  bobberObj.GetComponent<FishAttack>();
   //   fishAttack.FishEndAttack += EndAttack;
    }

    private void EndAttack()
    {
      _state = BobberState.InAir;
   //  fishOnHook = _bobber.PullUpBobber();
      CheckResult();
    }

    private IEnumerator BobberMoveUp()
    {
      yield return new WaitForSeconds(0.4f);

      for (int i = 0; i < 60; i++)
      {
        bobberObj.transform.position += new Vector3(0f, 0.2f, 0f);
        yield return new WaitForSeconds(0.01f);
      }
    }


    private void OnDestroy()
    {
      _playerProgress.MoneyData.Changed -= FishSold;
    }
  }
}