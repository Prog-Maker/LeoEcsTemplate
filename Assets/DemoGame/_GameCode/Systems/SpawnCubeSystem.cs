using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class SpawnCubeSystem : BaseSystem
    {
        private EcsFilter<SpawnerAnyObject> _spawner;
        private EcsFilter<SpawnPoint> _spawnPoints;

        float _timer = 0;

        public override void Run()
        {
            _timer += Time.deltaTime;

            if (_timer >= 1)
            {
                _timer = 0;
                RunFilter<SpawnerAnyObject>(_spawner, Spawn);
            }
        }

        private void Spawn(SpawnerAnyObject entitySpawner)
        {
            int index = Random.Range(0, _spawnPoints.GetEntitiesCount());
            var point = GetObject<SpawnPoint>(_spawnPoints, index);
            var cube = Object.Instantiate(entitySpawner.Prefab, point.Transform.position, Quaternion.identity, entitySpawner.ParentToSpawn);
        }
    }
}
