using System;

namespace Game
{
    [Serializable]
    public class UObject : object
    {
        public static UObject CreateInstance(Type type)
        {
            var instance = new UObject();
           
            return new UObject();
        }
    }
}
