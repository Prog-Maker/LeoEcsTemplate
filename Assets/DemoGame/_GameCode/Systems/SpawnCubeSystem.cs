using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class SpawnCubeSystem : IEcsRunSystem
    {
        private EcsFilter<SpawnerAnyObject> _spawner;
        private EcsFilter<SpawnPoint> _spawnPoints;

        float _timer = 0;

        public void Run()
        {
            _timer += Time.deltaTime;

            if (_timer >= 1)
            {
                _timer = 0;

                foreach (var sp in _spawner)
                {
                    ref var entitySpawner =  ref _spawner.GetEntity(sp).Get<SpawnerAnyObject>();

                    ref var point =  ref _spawnPoints.GetEntity(Random.Range(0, _spawnPoints.GetEntitiesCount())).Get<SpawnPoint>();

                    var cube = Object.Instantiate(entitySpawner.Prefab, point.Transform.position, Quaternion.identity, entitySpawner.ParentToSpawn);
                }
            }
        }
    }
}
