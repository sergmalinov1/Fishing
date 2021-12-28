using System;
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

    private BaseWindow _windows;

    public ResultState(LogicStateMachine logicStateMachine,
      IInputService inputService,
      IWindowService windowService,
      PlayerProgress playerProgress,
      ISaveLoadService saveLoadService)
    {
      _logicStateMachine = logicStateMachine;
      _windowService = windowService;
      _playerProgress = playerProgress;
      _saveLoadService = saveLoadService;
      _input = inputService;
    }

        public void Enter()
        {
            _windows = _windowService.Open(WindowId.Result);
            ChangeMoneyAndSave();
            // PlayFishUpAnimation();
        }

       

        public void Exit()
        {
            _windows.CloseWindow();
        }

        public void UpdateLogic()
        {
            if (_input.IsAttackButtonUp())
            {
                _logicStateMachine.Enter<BasicState>();
            }
        }
        private void ChangeMoneyAndSave()
        {
            if (_playerProgress.FishOnHook.IsFishOnHook)
                _playerProgress.MoneyData.Add(_playerProgress.FishOnHook.PrizeMoney);

            _saveLoadService.SaveProgress();
        }

        private void PlayFishUpAnimation()
        {
            _logicStateMachine.CameraControl.RotateCameraUp();
            _logicStateMachine.TackleContainer.DisableBobberAnimation();

            bool islineBreak = _playerProgress.FishOnHook.IsLineBreak;

            if(islineBreak)
            {
                _logicStateMachine.TackleContainer.MoveFromWaterAndBreak();
            }
            else
            {
                _logicStateMachine.TackleContainer.MoveFromWater();             
            }
        }

    }
}