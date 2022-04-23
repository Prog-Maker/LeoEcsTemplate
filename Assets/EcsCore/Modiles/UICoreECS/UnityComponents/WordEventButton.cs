using Leopotam.Ecs;
using UnityEngine;

namespace UICoreECS
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public abstract class WordEventButton : AUIEntity
    {
        [SerializeField] private UnityEngine.UI.Button _button;

        protected EcsWorld _world;
        protected EcsEntity _screen;

        private void Awake()
        {
            _button.onClick.AddListener(Do);
        }

        public override void Init(EcsWorld world, EcsEntity screen) 
        {
            _world = world;
            _screen = screen;
        }

        public abstract void Do();


#if UNITY_EDITOR
        private void Reset() 
        {
            _button = GetComponent<UnityEngine.UI.Button>();
        }
#endif
    }
}
