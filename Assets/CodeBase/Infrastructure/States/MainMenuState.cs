using CodeBase.Logic;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure.States
{
  public class MainMenuState : IPayloadedState<string>
  {
    private GameStateMachine _stateMachine;
    private SceneLoader _sceneLoader;
    private IUIFactory _uiFactory;

    public MainMenuState(GameStateMachine stateMachine,SceneLoader sceneLoader,  IUIFactory uiFactory)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _uiFactory = uiFactory;
 
    }
    
    public void Enter(string sceneName)
    {
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    private async void OnLoaded()
    {
     // await _uiFactory.CreateUIRoot();
   //   _uiFactory.CreateMainWindow();
    }

    public void Exit()
    {
    }
  }
}