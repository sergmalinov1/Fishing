using CodeBase.BobberObject;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.RandomService;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using CodeBase.StaticData.Fish;
using UnityEngine;

namespace CodeBase.GameLogic.States
{
  public class ThrowIntoWaterState : IStateLogic
  {
    private readonly LogicStateMachine _logicStateMachine;
    private readonly IGameFactory _gameFactory;
    private readonly PlayerProgress _playerProgress;
    private readonly IStaticDataService _staticData;
    private readonly IRandomService _randomService;
    private Camera _camera;
    
    private GameObject bobberObj;
    private Bobber _bobber;
    private float _coutDown = 2.0f;
    private bool _isStartAttack = false;

    public ThrowIntoWaterState(
      LogicStateMachine logicStateMachine, 
      IGameFactory gameFactory, 
      PlayerProgress playerProgress, 
      IStaticDataService staticData, 
      IRandomService randomService)
    {
      _logicStateMachine = logicStateMachine;
      _gameFactory = gameFactory;
      _playerProgress = playerProgress;
      _staticData = staticData;
      _randomService = randomService;
      _camera = Camera.main;
    }


        public void Enter()
        {

            _logicStateMachine.CameraControl.RotateCameraDown();
            _isStartAttack = true;
            _logicStateMachine.ContainerMoveDown();
            //  CreateBobber();
            SetCoutDownTime();
        }

        public void Exit()
    {
      
    }

    public void UpdateLogic()
    {
      _coutDown -= Time.deltaTime;
      if (_coutDown <= 0.0f)
      {
        SetCoutDownTime();

        _logicStateMachine.Enter<FishAttackState>(); //Необходимо переделать. 
      }
    }
    
    private async void CreateBobber()
    {
      float distanceFromCamera = 13.0f;
      float colibtateAngle = 4.0f; //Выравниваю поплавок по центру экрана
      float angle = _camera.transform.rotation.eulerAngles.y - colibtateAngle;

      //определение позиции в которой будет создан поплавок
      GameObject tempObj = new GameObject();
      tempObj.transform.position = new Vector3(_camera.transform.position.x , 0.0f, _camera.transform.position.z + distanceFromCamera);
      tempObj.transform.RotateAround(_camera.transform.position, Vector3.up, angle);
      
      //   Vector3 target = new Vector3(_camera.transform.position.x , 10.0f, _camera.transform.position.z + distanceFromCamera);

      _logicStateMachine.Bobber = await _gameFactory.CreateBobber(tempObj.transform.position);

      _logicStateMachine.BobberAnimator = _logicStateMachine.Bobber.GetComponent<BobberAnimator>();


      
    }


    private void SetCoutDownTime() => _coutDown = _randomService.TimeToBite();
  }
}