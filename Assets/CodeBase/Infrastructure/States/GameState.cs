using System.Threading.Tasks;
using CodeBase.GameLogic;
using CodeBase.Infrastructure.Factory;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class GameState : IState
  {
    private GameStateMachine _stateMachine;
    private SceneLoader _sceneLoader;
    private IGameFactory _gameFactory;
    private IUIFactory _uiFactory;

    public GameState(GameStateMachine stateMachine,SceneLoader sceneLoader,  IGameFactory gameFactory, IUIFactory uiFactory)
    {
     // _logicMachine = logicMachine;
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _uiFactory = uiFactory;
      _gameFactory = gameFactory;
    }
    public void Enter()
    {
    }

    private async void OnLoaded()
    {
      
    }
    

    public void Exit()
    {
    }
    
   
  }
}