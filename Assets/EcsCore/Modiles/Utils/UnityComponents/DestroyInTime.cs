using UnityEngine;
using System.Collections;

namespace Modules.Utils.UnityComponents
{
    public class DestroyInTime : MonoBehaviour
    {
        public float LifeTime = 1.0f;

        private void Awake()
        {
            StartCoroutine(Destructor());
        }

        public IEnumerator Destructor() 
        {
            yield return new WaitForSeconds(LifeTime);
            Destroy(this.gameObject);
        }

    }
}
