using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using CodeBase.UI.Services.WindowsService;
using System;
using UnityEngine;

namespace CodeBase.GameLogic.States
{
    public class PreparationState : IStateLogic
    {
        private IInputService _input;
        private readonly LogicStateMachine _logicMachine;
        private readonly IWindowService _windowService;
        private PlayerProgress _playerProgress;
        private readonly IGameFactory _gameFactory;
        private Camera _camera;

        public PreparationState(
          LogicStateMachine logicMachine,
          IInputService inputService,
          IWindowService windowService,
          PlayerProgress playerProgress,
          IGameFactory gameFactory)
        {
            _logicMachine = logicMachine;
            _input = inputService;
            _windowService = windowService;
            _playerProgress = playerProgress;
            _gameFactory = gameFactory;
            _camera = Camera.main;
        }

        public void Enter()
        {
            _playerProgress.FishOnHook.ClearBool();

            SettingTackleContainer();
            _logicMachine.TackleContainer.MoveToPlayer();
            _playerProgress.SettingWindow.StartGameLoop?.Invoke();
        }

      

        public void Exit()
        {
        }

        public void UpdateLogic()
        {
            if (_input.IsAttackButtonUp())
            {
                if (!IsSuitableFish())
                {
                   // Debug.Log("В озере нет подходящей рыбы");
                    _logicMachine.Enter<BasicState>();

                    _playerProgress.SettingWindow.MsgForPopup = Constants.MsgNotFishInLure;
                    _windowService.Open(WindowId.InfoPopup);
                    return;
                }

                if (_playerProgress.Inventory.IsEquipmentCompete())
                {
                    _logicMachine.Enter<ThrowIntoWaterState>();
                }
                else
                {
                    Debug.Log("Инвентарь не готов - PrepareState");
                    _playerProgress.SettingWindow.MsgForPopup = Constants.MsgEquipmentNotCompleted;
                    _windowService.Open(WindowId.InfoPopup);
                }
            }
        }

        // Проверка рыбы в озере. 
        // Если в озере нет рыбы которая питается выбранной наживкой, то списко EquipmentStats.Fishes - будет пустой. И метод вернет False
        // Если в озере ЕСТЬ рыба которая питается выбранной наживкой - метод вернет True
        private bool IsSuitableFish()
        {
            if (_playerProgress.EquipmentStats.Fishes.Count == 0)         
                return false;
            
            return true;
        }

        private async void SettingTackleContainer()
        {
            int bobberId = _playerProgress.Inventory.GetSelectedEquipmentByKind(KindEquipmentId.Bobber);

            float distanceFromCamera = 13.0f;
            float distanceFromWater = 12.0f;
            Vector3 containerPosition = new Vector3(_camera.transform.position.x, distanceFromWater, _camera.transform.position.z + distanceFromCamera);

            _logicMachine.TackleContainerObject.transform.position = containerPosition;

            _logicMachine.TackleContainer.Bobber = await _gameFactory.CreateBobberInContainer(_logicMachine.TackleContainer, bobberId);
            _logicMachine.TackleContainer.SetBobberAnimator(); 

            _logicMachine.TackleContainer.Lure = await _gameFactory.CreateLureInContainer(_logicMachine.TackleContainer, bobberId);

            _logicMachine.TackleContainer.Hook = await _gameFactory.CreateHookInContainer(_logicMachine.TackleContainer);

        }

        private async void DefinePosition()
        {
            float distanceFromCamera = 13.0f;
            float angle = _camera.transform.rotation.eulerAngles.y;

            //определение позиции в которой будет создан поплавок

           // _logicMachine.ContainerPosition = new GameObject();
           // _logicMachine.ContainerPosition.transform.position = new Vector3(_camera.transform.position.x, 12.0f, _camera.transform.position.z + distanceFromCamera);
          //  _logicMachine.ContainerPosition.transform.RotateAround(_camera.transform.position, Vector3.up, angle);

           
        }
    }
}