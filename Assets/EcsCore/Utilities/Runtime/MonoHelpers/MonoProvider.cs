using Sirenix.OdinInspector;
using UnityEngine;

namespace Leopotam.Ecs
{
    public abstract class MonoProvider<T> : BaseMonoProvider, IConvertToEntity where T : struct
    {
        [SerializeField]
        [HideInInspector]
        protected T _ComponentFields;


        [ShowInInspector]
        protected T ComponentFields
        {
            get
            {
                return _ComponentFields;
            }
            set
            {
                _ComponentFields = value;
                
                if (Application.isPlaying)
                {
                    _entity.Replace(_ComponentFields);
                }
            }
        }

        private EcsEntity _entity;

        void IConvertToEntity.Convert(EcsEntity entity)
        {
            _entity = entity;
            entity.Replace(ComponentFields);
        }
    }
}
