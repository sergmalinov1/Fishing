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
        private readonly IRandomService _randomService;

        private float _coutDown = 2.0f;

        public ThrowIntoWaterState(LogicStateMachine logicStateMachine, IRandomService randomService)
        {
            _logicStateMachine = logicStateMachine;
            _randomService = randomService;
        }

        public void Enter()
        {
            _logicStateMachine.CameraControl.RotateCameraDown();
            _logicStateMachine.TackleContainer.MoveToWater();

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

        private void SetCoutDownTime() => _coutDown = _randomService.TimeToBite();
    }
}