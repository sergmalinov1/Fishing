namespace CodeBase.Infrastructure.States
{
  public interface IExitableState
  {
    void Exit();
  }

  public interface IState : IExitableState
  {
    void Enter();
  }

  public interface IPayloadedState<TPayload> : IExitableState
  {
    void Enter(TPayload sceneName);
  }

  public interface IStateLogic : IState
  {
    void UpdateLogic();
  }
}