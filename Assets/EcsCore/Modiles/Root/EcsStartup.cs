using UnityEngine;
using Leopotam.Ecs;
using System.Collections.Generic;
using LeoEcsPhysics;

namespace Modules.Root
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private List<UnityEngine.Object> _systemProviders;

        private EcsWorld _world;
        private EcsSystems _systems;

        void OnEnable()
        {
            // Application.targetFrameRate = 60;

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            EcsPhysicsEvents.ecsWorld = _world;
            EcsSystems endFrame = new EcsSystems(_world, "EndFrame");

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems.ConvertScene();
            _systems.Add(new Utils.UnityTimeSystem());

            for (int i = 0; i < _systemProviders.Count; i++)
            {
                if(_systemProviders[i] is ISystemsProvider) 
                {
                    _systems.Add((_systemProviders[i] as ISystemsProvider).GetSystems(_world, endFrame, _systems));
                }
            }
            
            _systems.Add(endFrame);
            _systems.Inject(new Utils.TimeService());

            _systems
                .Init();
        }

#if UNITY_EDITOR

        [ContextMenu("Add")]
        public void Add() 
        {
            List<Object> temp = _systemProviders;
            _systemProviders = new List<Object>();
            _systemProviders.Add(null);
            _systemProviders.AddRange(temp);
        }

#endif
        void Update()
        {
            _systems.Run();
        }

        void OnDisable()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}
