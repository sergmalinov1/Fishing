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
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.UI.Services.WindowsService;
using UnityEngine;

namespace CodeBase.GameLogic
{
  public class LogicStateMachine : MonoBehaviour, ILogicStateMachine
  {
        public GameObject Bobber;
        public BobberAnimator BobberAnimator;
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
        private IStaticDataService _staticData;
        private ISaveLoadService _saveLoadService;
        private IRandomService _randomService;

        public void Construct(
          GameFactory gameFactory,
          IWindowService windowService,
          PlayerProgress playerProgress,
          IStaticDataService staticData,
          ISaveLoadService saveLoadService,
          IRandomService randomService)
        {
            SelectCamereControl();
            _gameFactory = gameFactory;
            _windowService = windowService;
            _input = AllServices.Container.Single<IInputService>();
            _playerProgress = playerProgress;
            _staticData = staticData;
            _saveLoadService = saveLoadService;
            _randomService = randomService;


            _states = new Dictionary<Type, IStateLogic>
            {
                [typeof(StartState)] = new StartState(this, _input, _randomService, _gameFactory),
                [typeof(PreparationState)] = new PreparationState(this, _input, _windowService, _playerProgress, _staticData, _gameFactory),
                [typeof(ThrowIntoWaterState)] = new ThrowIntoWaterState(this, _gameFactory, _playerProgress, _staticData, _randomService),
                [typeof(FishAttackState)] = new FishAttackState(this, _input, _playerProgress, _staticData, _randomService),
                [typeof(ResultState)] = new ResultState(this, _input, _windowService, _playerProgress, _saveLoadService, _gameFactory),
            };
        }

        public void Initialize()
        {
            Enter<StartState>();
        }

        private void Update()
        {
            _activeState.UpdateLogic();
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void ClenUp()
        {
            Destroy(Bobber);
            BobberAnimator = null;
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



        public void ContainerMoveDown(int count)
        {
            StartCoroutine(MoveDownContainer(count));
        }

        public void FishUP()
        {
            StartCoroutine(MoveUp());
        }

        public void FishUPAndDestrou()
        {
            StartCoroutine(MoveUpDestroy());
        }

        private IEnumerator MoveUp()
        {
            yield return new WaitForSeconds(0.6f);

            for (int i = 0; i < 60; i++)
            {
                Fish.transform.position += new Vector3(0f, 0.1f, 0f);
                yield return new WaitForSeconds(0.01f);
            }
        }

        private IEnumerator MoveUpDestroy()
        {
            for (int i = 0; i < 60; i++)
            {
                Fish.transform.position += new Vector3(0f, 0.1f, 0f);
                yield return new WaitForSeconds(0.01f);
            }

            Destroy(Fish);
        }

        private IEnumerator MoveDownContainer(int count)
        {
            for (int i = 0; i < count; i++)
            {
                TackleContainerObject.transform.position -= new Vector3(0f, 0.2f, 0f);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}