using CodeBase.Data;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using CodeBase.UI.Services.WindowsService;
using UnityEngine;

namespace CodeBase.GameLogic.States
{
  public class PreparationState : IStateLogic
  {
    private IInputService _input;
    private readonly LogicStateMachine _logicMachine;
    private readonly IWindowService _windowService;
    private readonly IStaticDataService _staticData;
    private PlayerProgress _playerProgress;
    
    public PreparationState(
      LogicStateMachine logicMachine, 
      IInputService inputService, 
      IWindowService windowService, 
      PlayerProgress playerProgress)
    {
      _logicMachine = logicMachine;
      _input = inputService;
      _windowService = windowService;
      _playerProgress = playerProgress;


    }

    public void Enter()
    {
      _playerProgress.FishOnHook.SelectedLure += SelectLure;
    }

    public void Exit()
    {
      _playerProgress.FishOnHook.SelectedLure -= SelectLure;
    }

    public void UpdateLogic()
    {
      if (_input.IsAttackButtonUp())
      {
        _windowService.Open(WindowId.PrepareWindow);
        
      }
    }

    private void SelectLure()
    {
      _logicMachine.Enter<ThrowIntoWaterState>();
    }
  }
}