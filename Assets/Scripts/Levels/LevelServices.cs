using Game;
using Game.AbilitySystem.AbilityUI;
using Services.Events;
using Services.Factory;
using Services.InputHandler;
using Services.Locator;
using Unity.VisualScripting;
using UnityEngine;

namespace Levels
{
    public class LevelServices : MonoBehaviour
    {
        [SerializeField] private AbilityDisplaying _abilityDisplaying;

        private ServiceLocator _serviceLocator;

        private PlayerInput _playerInput;
        private EntityFactory _entityFactory;
        private LevelEvents _levelEvents;

        public void InitiailizeServices(GameInstance gameInstance)
        {
            _serviceLocator = new ServiceLocator();

            _serviceLocator.RegisterService<GameInstance>(gameInstance);

            RegistrationOfServices();
            InjectServicesInSceneObjects();
        }

        private void RegistrationOfServices()
        {
            _playerInput = new PlayerInput();
            _levelEvents = new LevelEvents();
            _entityFactory = new EntityFactory(_serviceLocator, _levelEvents);

            _serviceLocator
                .RegisterService<ILevelEvents>(_levelEvents)
                .RegisterService<ILevelEventsExec>(_levelEvents)
                .RegisterService<EntityFactory>(_entityFactory)
                .RegisterService<PlayerInput>(_playerInput)
                .RegisterService<AbilityDisplaying>(_abilityDisplaying);
        }

        private void InjectServicesInSceneObjects()
        {
            foreach (var monoBehavior in FindObjectsOfType<MonoBehaviour>())
            {
                if(monoBehavior is IInjectServices dependency)
                {
                    dependency.Inject(_serviceLocator);
                }
            }
        }

        public IServiceLocator GetServiceLocator() => _serviceLocator;
    }
}
