using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Game
{
    public static class AppDomainExtension
    {
        public static Type[] GetAllTypes(this AppDomain appDomain)
        {
            return appDomain.GetAssemblies().GetAllTypes();
        }

        public static Type[] GetAllTypes(this Assembly assembly)
        {
            List<Type> list = new List<Type>();

            try
            {
                list.AddRange(assembly.GetTypes());
            }
            catch (ReflectionTypeLoadException ex)
            {
                list.AddRange(ex.Types.Where((Type type) => (object)type != null));
            }

            return list.ToArray();
        }

        public static Type[] GetAllTypes(this IEnumerable<Assembly> assemblies)
        {
            List<Type> list = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                try
                {
                    list.AddRange(assembly.GetTypes());
                }
                catch (ReflectionTypeLoadException ex)
                {
                    list.AddRange(ex.Types.Where((Type type) => (object)type != null));
                }
            }

            return list.ToArray();
        }

        public static Type[] GetNonAbstractTypes<T>(this AppDomain appDomain)
        {
            return appDomain.GetAllTypes().GetNonAbstractTypes<T>();
        }

        public static Type[] GetNonAbstractTypes<T>(this Type[] types)
        {
            return (from type in types
                    where !type.IsAbstract
                    where type.ImplementsInterface<T>()
                    select type).ToArray();
        }

        public static T[] GetInstancesOf<T>(this AppDomain appDomain)
        {
            return appDomain.GetNonAbstractTypes<T>().GetInstancesOf<T>();
        }

        public static T[] GetInstancesOf<T>(this Type[] types)
        {
            return (from type in types.GetNonAbstractTypes<T>()
                    select (T)Activator.CreateInstance(type)).ToArray();
        }

        private static Assembly _assembly = null;
        public static object[] GetRegisteredTypes()
        {
            if (_assembly == null)
            {
                _assembly = AppDomain.CurrentDomain.Load("Assembly-CSharp");
            }
            
            var allTypes = _assembly.GetAllTypes();

            object[] result = null;

            Type[] typesStruct =  allTypes.Where(type => type.IsValueType && !type.IsAbstract && type.Namespace == "Game").ToArray();

            result = new object[typesStruct.Length];

            int i = 0;
            foreach (var type in typesStruct)
            {
                result[i] = Activator.CreateInstance(type);
                i++;
            }

            return result;
        }
    }

    public static class InterfaceTypeExtension
    {
        public static bool ImplementsInterface<T>(this Type type)
        {
            if (!type.IsInterface)
            {
                return (object)type.GetInterface(typeof(T).FullName) != null;
            }

            return false;
        }
    }
}
