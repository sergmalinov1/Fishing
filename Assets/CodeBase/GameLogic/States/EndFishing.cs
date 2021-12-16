using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.GameLogic.States
{
    class EndFishing : IStateLogic
    {
        private readonly LogicStateMachine _logicMachine;
        private readonly IInputService _input;

        private float _timeToNextRound = 1.5f;

        public EndFishing(LogicStateMachine logicMachine, IInputService input)
        {
            _logicMachine = logicMachine;
            _input = input;
        }

        public void Enter()
        {
            _logicMachine.TackleContainer.MoveToBasicPosition();
            _timeToNextRound = 1.5f;
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
                    _logicMachine.Enter<PreparationState>();
                }
            }
        }
    }
}
