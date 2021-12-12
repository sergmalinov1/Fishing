using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.RandomService;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.GameLogic.States
{
    public class StartState : IStateLogic
    {
        private readonly LogicStateMachine _logicMachine;
        private readonly IInputService _input;
        private readonly IRandomService _randomService;


        public StartState(LogicStateMachine logicMachine, IInputService input, IRandomService randomService)
        {
            _logicMachine = logicMachine;
            _input = input;
            _randomService = randomService;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {

        }

        public void UpdateLogic()
        {
            if (_input.IsAttackButtonUp())
            {
                _logicMachine.Enter<PreparationState>();
            }
        }
    }
}