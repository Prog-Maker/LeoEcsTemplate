using UnityEngine;
using System;
using Component = UnityEngine.Component;
using System.Collections.Generic;
using Leopotam.Ecs;

namespace Game
{
    public static class ExtensionTools
    {
        public delegate void ActionRef<T>(ref T dataRow);
        public delegate void ActionRef<T1, T2>(ref T1 t1, ref T2 t2);
        public delegate void ActionRef<T1, T2, T3>(ref T1 t1, ref T2 t2, ref T3 t3);
        public delegate void ActionRef<T1, T2, T3, T4>(ref T1 t1, ref T2 t2, ref T3 t3, ref T4 t4);
        public delegate void ActionRef<T1, T2, T3, T4, T5>(ref T1 t1, ref T2 t2, ref T3 t3, ref T4 t4, ref T5 t5);
        public delegate void ActionRef<T1, T2, T3, T4, T5, T6>(ref T1 t1, ref T2 t2, ref T3 t3, ref T4 t4, ref T5 t5, ref T6 t6);

        public delegate void ActionEntityRef<T1>(ref T1 t1, ref EcsEntity entity);
        public delegate void ActionEntityRef<T1, T2>(ref T1 t1, ref T2 t2, ref EcsEntity entity);
        public delegate void ActionEntityRef<T1, T2, T3>(ref T1 t1, ref T2 t2, ref T3 t3, ref EcsEntity entity);
        public delegate void ActionEntityRef<T1, T2, T3, T4>(ref T1 t1, ref T2 t2, ref T3 t3, ref T4 t4, ref EcsEntity entity);
        public delegate void ActionEntityRef<T1, T2, T3, T4, T5>(ref T1 t1, ref T2 t2, ref T3 t3, ref T4 t4, ref T5 t5, ref EcsEntity entity);
        public delegate void ActionEntityRef<T1, T2, T3, T4, T5, T6>(ref T1 t1, ref T2 t2, ref T3 t3, ref T4 t4, ref T5 t5, ref T6 t6, ref EcsEntity entity);

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);
            }

            return source;
        }

        #region Return Component
        public static void ForEach<T>(this Leopotam.Ecs.EcsFilter<T> source, ActionRef<T> action) where T : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity.Get<T>());
            }
        }

        public static void ForEach<T1, T2>(this Leopotam.Ecs.EcsFilter<T1, T2> source, ActionRef<T1, T2> action)
          where T1 : struct
          where T2 : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity.Get<T1>(), ref entity.Get<T2>());
            }
        }

        public static void ForEach<T1, T2, T3>(this Leopotam.Ecs.EcsFilter<T1, T2, T3> source, ActionRef<T1, T2, T3> action)
           where T1 : struct
           where T2 : struct
           where T3 : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity.Get<T1>(), ref entity.Get<T2>(), ref entity.Get<T3>());
            }
        }

        public static void ForEach<T1, T2, T3, T4>(this Leopotam.Ecs.EcsFilter<T1, T2, T3, T4> source, ActionRef<T1, T2, T3, T4> action)
          where T1 : struct
          where T2 : struct
          where T3 : struct
          where T4 : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity.Get<T1>(), ref entity.Get<T2>(), ref entity.Get<T3>(), ref entity.Get<T4>());
            }
        }

        public static void ForEach<T1, T2, T3, T4, T5>(this Leopotam.Ecs.EcsFilter<T1, T2, T3, T4, T5> source, ActionRef<T1, T2, T3, T4, T5> action)
          where T1 : struct
          where T2 : struct
          where T3 : struct
          where T4 : struct
          where T5 : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity.Get<T1>(), ref entity.Get<T2>(), ref entity.Get<T3>(), ref entity.Get<T4>(), ref entity.Get<T5>());
            }
        }

        public static void ForEach<T1, T2, T3, T4, T5, T6>(this Leopotam.Ecs.EcsFilter<T1, T2, T3, T4, T5, T6> source, ActionRef<T1, T2, T3, T4, T5, T6> action)
          where T1 : struct
          where T2 : struct
          where T3 : struct
          where T4 : struct
          where T5 : struct
          where T6 : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity.Get<T1>(), ref entity.Get<T2>(), ref entity.Get<T3>(), ref entity.Get<T4>(), ref entity.Get<T5>(), ref entity.Get<T6>());
            }
        }
        #endregion

        #region Return Entity
        public static void ForEach<T>(this Leopotam.Ecs.EcsFilter<T> source, ActionRef<Leopotam.Ecs.EcsEntity> action) where T : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity);
            }
        }

        public static void ForEach<T1, T2>(this Leopotam.Ecs.EcsFilter<T1, T2> source, ActionRef<Leopotam.Ecs.EcsEntity> action)
            where T1 : struct
            where T2 : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity);
            }
        }

        public static void ForEach<T1, T2, T3>(this Leopotam.Ecs.EcsFilter<T1, T2, T3> source, ActionRef<Leopotam.Ecs.EcsEntity> action)
           where T1 : struct
           where T2 : struct
           where T3 : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity);
            }
        }

        public static void ForEach<T1, T2, T3, T4>(this Leopotam.Ecs.EcsFilter<T1, T2, T3, T4> source, ActionRef<Leopotam.Ecs.EcsEntity> action)
          where T1 : struct
          where T2 : struct
          where T3 : struct
          where T4 : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity);
            }
        }

        public static void ForEach<T1, T2, T3, T4, T5>(this Leopotam.Ecs.EcsFilter<T1, T2, T3, T4, T5> source, ActionRef<Leopotam.Ecs.EcsEntity> action)
          where T1 : struct
          where T2 : struct
          where T3 : struct
          where T4 : struct
          where T5 : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity);
            }
        }

        public static void ForEach<T1, T2, T3, T4, T5, T6>(this Leopotam.Ecs.EcsFilter<T1, T2, T3, T4, T5, T6> source, ActionRef<Leopotam.Ecs.EcsEntity> action)
          where T1 : struct
          where T2 : struct
          where T3 : struct
          where T4 : struct
          where T5 : struct
          where T6 : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity);
            }
        }

        #endregion

        #region Return Component And Entity
        public static void ForEach<T>(this Leopotam.Ecs.EcsFilter<T> source, ActionEntityRef<T> action) where T : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity.Get<T>(), ref entity);
            }
        }

        public static void ForEach<T1, T2>(this Leopotam.Ecs.EcsFilter<T1, T2> source, ActionEntityRef<T1, T2> action)
          where T1 : struct
          where T2 : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity.Get<T1>(), ref entity.Get<T2>(), ref entity);
            }
        }

        public static void ForEach<T1, T2, T3>(this Leopotam.Ecs.EcsFilter<T1, T2, T3> source, ActionEntityRef<T1, T2, T3> action)
           where T1 : struct
           where T2 : struct
           where T3 : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity.Get<T1>(), ref entity.Get<T2>(), ref entity.Get<T3>(), ref entity);
            }
        }

        public static void ForEach<T1, T2, T3, T4>(this Leopotam.Ecs.EcsFilter<T1, T2, T3, T4> source, ActionEntityRef<T1, T2, T3, T4> action)
          where T1 : struct
          where T2 : struct
          where T3 : struct
          where T4 : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity.Get<T1>(), ref entity.Get<T2>(), ref entity.Get<T3>(), ref entity.Get<T4>(), ref entity);
            }
        }

        public static void ForEach<T1, T2, T3, T4, T5>(this Leopotam.Ecs.EcsFilter<T1, T2, T3, T4, T5> source, ActionEntityRef<T1, T2, T3, T4, T5> action)
          where T1 : struct
          where T2 : struct
          where T3 : struct
          where T4 : struct
          where T5 : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity.Get<T1>(), ref entity.Get<T2>(), ref entity.Get<T3>(), ref entity.Get<T4>(), ref entity.Get<T5>(), ref entity);
            }
        }

        public static void ForEach<T1, T2, T3, T4, T5, T6>(this Leopotam.Ecs.EcsFilter<T1, T2, T3, T4, T5, T6> source, ActionEntityRef<T1, T2, T3, T4, T5, T6> action)
          where T1 : struct
          where T2 : struct
          where T3 : struct
          where T4 : struct
          where T5 : struct
          where T6 : struct
        {
            foreach (int i in source)
            {
                ref var entity = ref source.GetEntity(i);
                action(ref entity.Get<T1>(), ref entity.Get<T2>(), ref entity.Get<T3>(), ref entity.Get<T4>(), ref entity.Get<T5>(), ref entity.Get<T6>(), ref entity);
            }
        }
        #endregion
    }

    public class Tools : MonoBehaviour
    {
        public static T GetDataFromPrefs<T>(string key)
        {
            string json = PlayerPrefs.GetString(key, "");
            return JsonToObject<T>(json);
        }

        public static void SetDataToPrefs<T>(string key, T data)
        {
            string json = ObjectToJson(data);
            PlayerPrefs.SetString(key, json);
        }

        public static T JsonToObject<T>(string json)
        {
            try
            {
                return JsonUtility.FromJson<T>(json);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static string ObjectToJson<T>(T data)
        {
            return JsonUtility.ToJson(data);
        }

        public static T AddObject<T>(Transform parent) where T : Component
        {
            return AddObject<T>(null, parent, false);
        }

        public static T AddObject<T>(object obj, Transform parent, bool active = false) where T : Component
        {
            GameObject itemObj = null;

            if (obj == null)
            {
                itemObj = new GameObject();
                itemObj.AddComponent<T>();
            }
            else
            {
                if (obj.GetType() == typeof(string))
                {
                    T prefObj = Resources.Load<T>(obj.ToString());
                    itemObj = AddObject(prefObj.gameObject, parent);
                }
                else
                {
                    itemObj = AddObject(((T)obj).gameObject, parent);
                    if (itemObj.GetComponent<T>() == null)
                    {
                        itemObj.AddComponent<T>();
                    }
                }
            }

            itemObj.transform.SetParent(parent);

            SetZero(itemObj);

            if (active)
                itemObj.gameObject.SetActive(true);

            return itemObj.GetComponent<T>();
        }

        public static GameObject AddObject(Transform parent)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(parent);
            SetZero(obj);
            return obj;
        }

        public static GameObject AddObject(string objName, Transform parent, bool active = false)
        {
            GameObject obj = Resources.Load<GameObject>(objName);
            return AddObject(obj, parent, active);
        }

        public static GameObject AddObject(GameObject obj, Transform parent, bool active = false, bool scaleToOne = true)
        {

            GameObject itemObj = (GameObject) Instantiate(obj);
            itemObj.transform.SetParent(parent);
            SetZero(itemObj, scaleToOne);

            if (active)
                itemObj.gameObject.SetActive(true);

            return itemObj;
        }


        private static void SetZero(GameObject obj, bool scaleToOne = true)
        {
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            if (scaleToOne) obj.transform.localScale = Vector3.one;

            RectTransform rt = obj.GetComponent<RectTransform>();
            if (rt != null)
            {
                rt.anchoredPosition3D = Vector3.zero;
            }
        }

        public static void RemoveObjects(Transform root, bool immediate = false)
        {
            if (root.childCount == 0) return;

            int counter = immediate ? 10 : 1;
            for (int i = 0; i < counter; i++)
            {
                foreach (Transform child in root)
                {
                    child.gameObject.SetActive(false);
                    if (immediate)
                        DestroyImmediate(child.gameObject);
                    else
                        Destroy(child.gameObject);
                }
            }
        }

        public static T CopyComponent<T>(T original, GameObject destination) where T : Component
        {
            var type = original.GetType();
            var dst = destination.GetComponent(type) as T;
            if (!dst) dst = destination.AddComponent(type) as T;

            var fields = type.GetFields();
            foreach (var field in fields)
            {
                if (field.IsStatic) continue;
                field.SetValue(dst, field.GetValue(original));
            }

            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (!prop.CanWrite || prop.Name == "name") continue;
                prop.SetValue(dst, prop.GetValue(original, null), null);
            }

            return dst;
        }

        public static string[] ParseStr(string data, string split)
        {
            if (data == null)
                data = "";
            string[] array = data.Split(new string[] {split}, StringSplitOptions.None);
            return array;
        }

        public static Transform FindChild(Transform obj, string childName)
        {
            Transform[] childs = obj.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in childs)
            {
                if (child.name == childName)
                    return child;
            }

            return null;
        }

        public static Transform ContainsChild(Transform obj, string childName)
        {
            Transform[] childs = obj.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in childs)
            {
                if (child.name.Contains(childName))
                    return child;
            }

            return null;
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            float start = (min + max) * 0.5f - 180;
            float floor = Mathf.FloorToInt((angle - start) / 360) * 360;
            min += floor;
            max += floor;
            return Mathf.Clamp(angle, min, max);
        }

        //public static void SetIgnoreSelfCollisions(_GAME.Enemy.EnemyRefs enemy)
        //{
        //    if (!enemy.Ragdoll) return;

        //    var colliders = enemy.Ragdoll.Colliders;
        //    colliders.Add(enemy.Ragdoll.MainCollider);

        //    for (int i = 0; i < colliders.Count; i++)
        //    {
        //        for (int j = i + 1; j < colliders.Count; j++)
        //        {
        //            if (colliders[i] == colliders[j])
        //                continue;
        //            Physics.IgnoreCollision(colliders[i], colliders[j]);
        //        }
        //    }
        //}
    }
}


