using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.Utils.UnityComponents
{
    public class TapAnimTrigger : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private string _triggerKey;
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            if(_animator == null) 
            {
                this.gameObject.GetComponentInChildren<Animator>();
                Debug.LogWarning("Animator not set", this);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _animator.SetTrigger(_triggerKey);
        }


#if UNITY_EDITOR
        public void Reset() 
        {
            _animator = this.gameObject.GetComponentInChildren<Animator>();
        }
#endif
    }
}
