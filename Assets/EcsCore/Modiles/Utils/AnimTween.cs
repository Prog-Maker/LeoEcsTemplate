using UnityEngine;
using System.Collections;

namespace Modules.Utils
{
    [System.Serializable]
    public class AnimTween : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animKey;
        [SerializeField] private float _endTime;

        private Coroutine _stopper;

        public void Do() 
        {
            if(_stopper != null) 
            {
                StopCoroutine(_stopper);
            }

            _animator.enabled = true;
            _animator.Play(_animKey, 0, 0.0f);
            _stopper = StartCoroutine(Stopper());
        }

        private IEnumerator Stopper() 
        {
            yield return new WaitForSeconds(_endTime);
            _animator.enabled = false;
            _stopper = null;
        }

    }
}
