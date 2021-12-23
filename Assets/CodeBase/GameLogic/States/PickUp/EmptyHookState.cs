using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.GameLogic.States
{
    public class EmptyHookState : IStateLogic
    {
        private readonly LogicStateMachine _logicStateMachine;

        public EmptyHookState(LogicStateMachine logicStateMachine)
        {
            _logicStateMachine = logicStateMachine;
        }

        public void Enter()
        {
            _logicStateMachine.CameraControl.RotateCameraUp();
            _logicStateMachine.TackleContainer.DisableBobberAnimation();
            _logicStateMachine.TackleContainer.DestroyLure();
            _logicStateMachine.TackleContainer.MoveFromWater();

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
