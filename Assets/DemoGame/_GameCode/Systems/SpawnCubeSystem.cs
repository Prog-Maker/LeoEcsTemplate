using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class SpawnCubeSystem : IEcsRunSystem
    {
        EcsFilter<SpawnerAnyObject> _spawner = null;
        EcsFilter<SpawnPoint> _spawnPoints = null;
        EcsFilter<Player> _player = null;

        float _timer = 0;

        public void Run()
        {
            _timer += Time.deltaTime;

            if (_timer >= 1)
            {
                _timer = 0;

                _spawner.ForEach(ActionWithEntity);
            }
        }

        private void ActionWithComponent(ref SpawnerAnyObject entitySpawner)
        {
            ref var point =  ref _spawnPoints.GetEntity(Random.Range(0, _spawnPoints.GetEntitiesCount())).Get<SpawnPoint>();
            var cube = Object.Instantiate(entitySpawner.Prefab, point.Transform.position, Quaternion.identity, entitySpawner.ParentToSpawn);
        }

        private void ActionWithEntity(ref EcsEntity entity)
        {
            ref var entitySpawner = ref entity.Get<SpawnerAnyObject>();
            ref var point =  ref _spawnPoints.GetEntity(Random.Range(0, _spawnPoints.GetEntitiesCount())).Get<SpawnPoint>();
            var cube = Object.Instantiate(entitySpawner.Prefab, point.Transform.position, Quaternion.identity, entitySpawner.ParentToSpawn);
        }
    }
}
