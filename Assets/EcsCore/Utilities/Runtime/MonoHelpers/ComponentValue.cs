using System;
using Sirenix.OdinInspector;

namespace Game
{
    [Serializable]
    public class ComponentValue<T>
    {
        private T _value;

        [ShowInInspector]
        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
