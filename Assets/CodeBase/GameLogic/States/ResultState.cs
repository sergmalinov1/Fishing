using System.Collections;
using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using CodeBase.StaticData.Fish;
using CodeBase.UI.Services.WindowsService;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.GameLogic.States
{
  public class ResultState : IStateLogic
  {
    private readonly LogicStateMachine _logicStateMachine;
    private readonly IWindowService _windowService;
    private readonly PlayerProgress _playerProgress;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IInputService _input;
    private readonly IGameFactory _gameFactory;

    private BaseWindow _windows;

    public ResultState(LogicStateMachine logicStateMachine,
      IInputService inputService,
      IWindowService windowService,
      PlayerProgress playerProgress,
      ISaveLoadService saveLoadService, IGameFactory gameFactory)
    {
      _logicStateMachine = logicStateMachine;
      _windowService = windowService;
      _playerProgress = playerProgress;
      _saveLoadService = saveLoadService;
      _input = inputService;
      _gameFactory = gameFactory;
    }

        public void Enter()
        {
            CreateFish();

            _logicStateMachine.CameraControl.RotateCameraUp();

            _logicStateMachine.TackleContainer.DisableBobberAnimation();
            _logicStateMachine.TackleContainer.MoveFromWater();

            _windows = _windowService.Open(WindowId.Result);
        }

        private async void CreateFish()
        {
            FishTypeId fishId = _playerProgress.FishOnHook.FishTypeId;
            _logicStateMachine.TackleContainer.Fish = await _gameFactory.CreateFishInContainer(_logicStateMachine.TackleContainer, fishId);
        }

        public void Exit()
        {
            _windows.CloseWindow();

           // _logicStateMachine.TackleContainer.NextFishingRound();

            // _logicStateMachine.CameraControl.RotateCameraDown();

            // _logicStateMachine.FishUPAndDestrou();

        }

        public void UpdateLogic()
        {
            if (_input.IsAttackButtonUp())
            {
                _playerProgress.MoneyData.Add(_playerProgress.FishOnHook.PrizeMoney);
                _saveLoadService.SaveProgress();
                _logicStateMachine.Enter<EndFishing>();
            }
        }


    }
}