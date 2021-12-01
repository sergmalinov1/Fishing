using System.Threading.Tasks;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;
    private readonly IUIFactory _uiFactory;
    
    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
      IGameFactory gameFactory, IUIFactory uiFactory)
    {
      _gameStateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _gameFactory = gameFactory;
      _uiFactory = uiFactory;
    }
    public void Enter(string sceneName)
    {
      _curtain.Show();
      
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
      _curtain.Hide();
    }
    private async void OnLoaded()
    {
      await InitUIRoot();
      await InitHud();
      await InitGameWorld();
      _gameStateMachine.Enter<GameState>();
    }

    private async Task InitUIRoot() => 
      await _uiFactory.CreateUIRoot();
    
    private async Task InitHud()
    {
      await _gameFactory.CreateHud();
      // hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
    }
    
    private async Task InitGameWorld()
    {
     // _gameFactory.CreareBobberSpawner();
     _gameFactory.CreareFishingLogic();
    }
  }
}