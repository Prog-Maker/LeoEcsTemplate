using UnityEngine;

namespace Modules.Utils
{
    [System.Serializable]
    public struct RagdollComponent
    {
        [SerializeField] public Collider[] Colliders;
        [SerializeField] public Rigidbody[] Rigidbodies;
        [SerializeField] public Animator Animator;
        [SerializeField] public Collider HitTrigger;

        public void SetMode(bool enabled) 
        {
            for (int i = 0; i < Colliders.Length; i++)
            {
                Colliders[i].enabled = enabled;
            }

            for (int i = 0; i < Rigidbodies.Length; i++)
            {
                Rigidbodies[i].isKinematic = !enabled;
            }

            Animator.enabled = !enabled;
            if (HitTrigger != null)
                HitTrigger.enabled = !enabled;
        }
    }
}
