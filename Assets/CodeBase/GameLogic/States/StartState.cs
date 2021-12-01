using CodeBase.Infrastructure.RandomService;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.GameLogic.States
{
  public class StartState: IStateLogic
  {
    private readonly LogicStateMachine _logicMachine;
    private readonly IRandomService _randomService;


    public StartState(LogicStateMachine logicMachine, IRandomService randomService)
    {
      _logicMachine = logicMachine;
      _randomService = randomService;
    }

    public void Enter()
    {
      _logicMachine.Enter<PreparationState>();
    }

    public void Exit()
    {

    }

    public void UpdateLogic()
    {
     
    }
  }
}