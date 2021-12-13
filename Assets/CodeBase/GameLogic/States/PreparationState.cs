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
        private readonly IStaticDataService _staticData;
        private PlayerProgress _playerProgress;
        private readonly IGameFactory _gameFactory;
        private Camera _camera;

        public PreparationState(
          LogicStateMachine logicMachine,
          IInputService inputService,
          IWindowService windowService,
          PlayerProgress playerProgress,
          IStaticDataService staticData,
          IGameFactory gameFactory)
        {
            _logicMachine = logicMachine;
            _input = inputService;
            _windowService = windowService;
            _playerProgress = playerProgress;
            _gameFactory = gameFactory;
            _staticData = staticData;
            _camera = Camera.main;
        }

        public void Enter()
        {
            _playerProgress.FishOnHook.SelectedLure += SelectLure;

            CreateEquipmentContainer();

            _logicMachine.ContainerMoveDown(35);
        }

      

        public void Exit()
        {
            _playerProgress.FishOnHook.SelectedLure -= SelectLure;
        }

        public void UpdateLogic()
        {
            if (_input.IsAttackButtonUp())
            {
                if (_playerProgress.Inventory.IsEquipmentCompete())
                {
                    _logicMachine.Enter<ThrowIntoWaterState>();
                }
                else
                {
                    _windowService.Open(WindowId.PrepareWindow);
                }
            }
        }

        private void SelectLure()
        {
            _logicMachine.Enter<ThrowIntoWaterState>();
        }

        private async void CreateEquipmentContainer()
        {
            int equipmentId = _playerProgress.Inventory.GetSelectedEquipmentByKind(KindEquipmentId.Bobber);

            float distanceFromCamera = 13.0f;
            float distanceFromWater = 12.0f;
            Vector3 containerPosition = new Vector3(_camera.transform.position.x, distanceFromWater, _camera.transform.position.z + distanceFromCamera);

            _logicMachine.EquipmentContainer = await _gameFactory.CreateEquipmentContainer(containerPosition, equipmentId);

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