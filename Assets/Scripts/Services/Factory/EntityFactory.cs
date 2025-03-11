using Game.Entities;
using Services.Events;
using Services.Locator;
using UnityEngine;

namespace Services.Factory
{
    public class EntityFactory
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly ILevelEventsExec _levelEventsExec;

        public EntityFactory(IServiceLocator serviceLocator, ILevelEventsExec levelEventsExec)
        {
            _serviceLocator = serviceLocator;
            _levelEventsExec = levelEventsExec;
        }

        public Actor CreateActor(Actor actor, Vector3 location, Quaternion rotation)
        {
            Actor actorInstance = Object.Instantiate(actor, location, rotation);

            foreach (var dependency in actorInstance.GetComponentsInChildren<IInjectServices>())
            {
                dependency.Inject(_serviceLocator);
            }

            return actorInstance;
        }
    }
}
