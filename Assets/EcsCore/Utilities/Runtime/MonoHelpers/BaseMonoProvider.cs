using UnityEngine;

namespace Leopotam.Ecs
{
    [RequireComponent(typeof (ConvertToEntity))]
    [RequireComponent(typeof (EntityRef))]
    public abstract class BaseMonoProvider : MonoBehaviour { }
}
