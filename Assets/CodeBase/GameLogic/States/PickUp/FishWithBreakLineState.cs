using CodeBase.Infrastructure.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.GameLogic.States
{
    public class FishWithBreakLineState : IStateLogic
    {
        private readonly LogicStateMachine _logicStateMachine;
        private float _timeToNextRound = 2f;

        public FishWithBreakLineState(LogicStateMachine logicStateMachine)
        {
            _logicStateMachine = logicStateMachine;
        }

        public void Enter()
        {
            _logicStateMachine.CameraControl.RotateCameraUp();
            _logicStateMachine.TackleContainer.DisableBobberAnimation();
            _logicStateMachine.TackleContainer.DestroyLure();
            _logicStateMachine.TackleContainer.MoveFromWaterAndBreak();

            _logicStateMachine.Enter<ResultState>();
        }

        public void Exit()
        {
            
        }

        public void UpdateLogic()
        {
      
        }
    }
}
