using CodeBase.Infrastructure.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.GameLogic.States
{
    public class HookWithLureState : IStateLogic
    {
        private readonly LogicStateMachine _logicStateMachine;

        public HookWithLureState(LogicStateMachine logicStateMachine)
        {
            _logicStateMachine = logicStateMachine;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }

        public void UpdateLogic()
        {
           
        }
    }
}
