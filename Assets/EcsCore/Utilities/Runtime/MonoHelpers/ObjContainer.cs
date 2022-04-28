using System;
using Sirenix.OdinInspector;

namespace Game
{
    [Serializable]
    [InlineProperty]
    [HideReferenceObjectPicker]
    [HideLabel]
    public class ObjContainer
    {
        private UObject _component;

        [ShowInInspector]
        [HideLabel]
        public UObject Component
        {
            get { return _component; }
            set { _component = value; }
        }
    }
}
