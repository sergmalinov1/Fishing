using CodeBase.Infrastructure.Factory;
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
        private readonly IGameFactory _gameFactory;

        public StartState(
            LogicStateMachine logicMachine, 
            IInputService input, 
            IRandomService randomService, 
            IGameFactory gameFactory)
        {
            _logicMachine = logicMachine;
            _input = input;
            _randomService = randomService;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            CreateTackleContainer();   
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

        private async void CreateTackleContainer()
        {
            Vector3 startPosition = new Vector3(0, 20f, 0);
            _logicMachine.TackleContainerObject = await _gameFactory.CreateTackleContainer(startPosition);
            _logicMachine.TackleContainer = _logicMachine.TackleContainerObject.GetComponent<TackleContainer>();
        }
    }
}