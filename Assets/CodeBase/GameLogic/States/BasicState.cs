using CodeBase.Data;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.States;
using CodeBase.UI.Services.WindowsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.GameLogic.States
{
    class BasicState : IStateLogic
    {
        private readonly LogicStateMachine _logicMachine;
        private readonly IInputService _input;
        private readonly IWindowService _windowService;
        private readonly IPersistentProgress _progressService;

        private float _timeToNextRound = 1f;

        public BasicState(LogicStateMachine logicMachine, IInputService input, IWindowService windowService, IPersistentProgress progressService)
        {
            _logicMachine = logicMachine;
            _input = input;
            _windowService = windowService;
            _progressService = progressService;
        }

        public void Enter()
        {
            _progressService.Progress.SettingWindow.EndGameLoop?.Invoke();
            _logicMachine.TackleContainer.MoveToBasicPosition();
            _logicMachine.TackleContainer.DestroyLure();
            _logicMachine.TackleContainer.DestroyHook();
            _timeToNextRound = 1f;
        }

        public void Exit()
        {
        }

        public void UpdateLogic()
        {


            _timeToNextRound -= Time.deltaTime;
            if (_timeToNextRound <= 0.0f)
            {
                if (_input.IsAttackButtonUp())
                {
                    if (_progressService.Progress.Inventory.IsEquipmentCompete())
                    {
                        _logicMachine.Enter<PreparationState>();
                    }
                    else
                    {
                        Debug.Log("Не выбраны все элементы в инвентаре");

                        _progressService.Progress.SettingWindow.MsgForPopup = Constants.MsgEquipmentNotCompleted;
                        _windowService.Open(WindowId.InfoPopup);
                    }                  
                }
            }
        }




    }
}
