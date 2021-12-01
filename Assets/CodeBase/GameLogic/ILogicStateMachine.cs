using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;

namespace CodeBase.GameLogic
{
  public interface ILogicStateMachine //: IService
  {
    void Enter<TState>() where TState : class, IState;
  }
}