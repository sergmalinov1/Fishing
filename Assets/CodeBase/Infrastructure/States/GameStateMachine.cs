using System;
using System.Collections.Generic;
using CodeBase.GameLogic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Inventory;
using CodeBase.Infrastructure.RandomService;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
        [typeof(LoadProgressState)] = new LoadProgressState(this, sceneLoader, curtain, 
          services.Single<IPersistentProgress>(),
          services.Single<ISaveLoadService>(),
          services.Single<IInventoryService>(),
          services.Single<IRandomService>()),

        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, 
          services.Single<IGameFactory>(), services.Single<IUIFactory>()),
    
        [typeof(GameState)] = new GameState(this, sceneLoader,
          services.Single<IGameFactory>(),
          services.Single<IUIFactory>()), //services.Single<ILogicStateMachine>()),
        
        [typeof(MainMenuState)] = new MainMenuState(this, sceneLoader,
          services.Single<IUIFactory>()),
      };
    }
    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayLoad>(TPayLoad payload) where TState : class, IPayloadedState<TPayLoad>
    {
      IPayloadedState<TPayLoad> state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();

      TState state = GetState<TState>();
      _activeState = state;

      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;
    
  }
}