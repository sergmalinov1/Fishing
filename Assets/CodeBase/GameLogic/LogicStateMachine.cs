using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.BobberObject;
using CodeBase.Data;
using CodeBase.GameLogic.States;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.RandomService;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.UI.Services.WindowsService;
using UnityEngine;

namespace CodeBase.GameLogic
{
  public class LogicStateMachine : MonoBehaviour, ILogicStateMachine
  {
        public RotateCamera CameraControl;

        public GameObject Fish;

        public GameObject TackleContainerObject;
        public TackleContainer TackleContainer;

        private Dictionary<Type, IStateLogic> _states;
        private IStateLogic _activeState;

        private IGameFactory _gameFactory;
        private IWindowService _windowService;
        private IInputService _input;

        private PlayerProgress _playerProgress;
        private IPersistentProgress _progressService;

        private IStaticDataService _staticData;
        private ISaveLoadService _saveLoadService;
        private IRandomService _randomService;

        public void Construct(
          GameFactory gameFactory,
          IWindowService windowService,
          IPersistentProgress progressService,
          IStaticDataService staticData,
          ISaveLoadService saveLoadService,
          IRandomService randomService)
        {
            SelectCamereControl();
            _gameFactory = gameFactory;
            _windowService = windowService;
            _input = AllServices.Container.Single<IInputService>();
            _playerProgress = progressService.Progress;
            _progressService = progressService;
            _staticData = staticData;
            _saveLoadService = saveLoadService;
            _randomService = randomService;


            _states = new Dictionary<Type, IStateLogic>
            {
                [typeof(StartState)] = new StartState(this, _gameFactory),
                [typeof(BasicState)] = new BasicState(this, _input, _windowService, _progressService),
                [typeof(PreparationState)] = new PreparationState(this, _input, _windowService, _playerProgress, _gameFactory),
                [typeof(ThrowIntoWaterState)] = new ThrowIntoWaterState(this, _randomService),
                [typeof(FishAttackState)] = new FishAttackState(this, _input, _progressService, _randomService,  _gameFactory, _saveLoadService),
                [typeof(ResultState)] = new ResultState(this, _input, _windowService, _playerProgress, _saveLoadService),

                [typeof(EmptyHookState)] = new EmptyHookState(this),
                [typeof(FishOnHookState)] = new FishOnHookState(this),
                [typeof(FishWithBreakLineState)] = new FishWithBreakLineState(this),
                [typeof(HookWithLureState)] = new HookWithLureState(this)
            };
        }

        public void Initialize()
        {
            Enter<StartState>();
        }

        private void FixedUpdate()
        {
            _activeState.UpdateLogic();
        }


        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }


        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = (IStateLogic)state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
          _states[typeof(TState)] as TState;

        private void SelectCamereControl()
        {
            Camera camera = Camera.main;
            CameraControl = camera.GetComponent<RotateCamera>();
        }

      
    }
}