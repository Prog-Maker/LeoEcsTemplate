using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.Utils
{
    public class TapFX : MonoBehaviour, IPointerDownHandler
    {
        public GameObject Effect;

        public void OnPointerDown(PointerEventData eventData)
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit)) 
            {
                Object.Instantiate(Effect, hit.point, Quaternion.identity);
            }
        }
    }
}
