using UnityEngine;

namespace Leopotam.Ecs
{
    public enum ConvertMode
    {
        ConvertAndInject,
        ConvertAndDestroy,
        ConvertAndSave
    }

    public class ConvertToEntity : MonoBehaviour
    {
        public ConvertMode ConvertMode;
        private EcsEntity? _entity;
        
        private bool _isProccessed = false;

        private void Start()
        {
            var world = WorldHandler.GetWorld();
            
            if (world != null && !_isProccessed)
            {
                var instantiateEntity = world.NewEntity();
                var instantiateComponent = new InstantiateComponent() { GameObject = gameObject };

                instantiateEntity.Replace(instantiateComponent);
            }
        }

        public EcsEntity? TryGetEntity()
        {
            if (_entity.HasValue)
            {
                if (_entity.Value.IsAlive())
                {
                    return _entity.Value;
                }
            }

            return null;
        }
        
        public void SetProccessed()
        {
            _isProccessed = true;
        }

        public void Set(EcsEntity entity)
        {
            _entity = entity;
        }
    }
}