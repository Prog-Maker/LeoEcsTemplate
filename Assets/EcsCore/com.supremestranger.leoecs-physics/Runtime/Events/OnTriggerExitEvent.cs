using UnityEngine;

namespace LeoEcsPhysics
{
    public struct OnTriggerExitEvent
    {
        public GameObject SenderGameObject;
        public Collider Collider;
    }
}