
namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;
    using UnityEngine;

    public static class Extension
    {
        public static T ReturnLast<T>(this List<T> itemList)
        {
            T t = itemList[itemList.Count - 1];
            itemList.RemoveAt(itemList.Count - 1);
            return t;
        }

        public static T ReturnItem<T>(this List<T> itemList, T t)
        {
            if (itemList.Contains(t))
            {
                int itemIndex = itemList.IndexOf(t);
                T temp = itemList[itemIndex];
                itemList.RemoveAt(itemIndex);
                return temp;
            }
            return default;
        }
        
        public static T ReturnItemAtIndex<T>(this List<T> itemList,int index)
        {
            if ((itemList.Count-1)<=index)
            {
                T temp = itemList[index];
                itemList.RemoveAt(index);
                return temp;
            }
            return default;
        }

        public static Dictionary<string, AnimationPropagator> ConvertToDictionary(
            this List<KeyValuePair<string, AnimationPropagator>> propogator)
        {
            Dictionary<string, AnimationPropagator> propagators = new Dictionary<string, AnimationPropagator>();
            for (int indexOfPropogator = 0; indexOfPropogator < propogator.Count; indexOfPropogator++)
            {
                propagators.Add(propogator[indexOfPropogator].key, propogator[indexOfPropogator].value);
            }

            return propagators;
        }

        public static List<SliceConfig> FilterSliceConfig(this List<SliceConfig> sliceConfigs,List<SliceConfig> slicesToRemove)
        {
            List<SliceConfig> sliceConfigToRemove = new List<SliceConfig>();

            foreach (var sliceConfig in sliceConfigs)
            {
                bool foundSliceConfig =false;
                foreach (var sliceToRemove in slicesToRemove)
                {
                    if (sliceToRemove.slice.GetInstanceID() == sliceConfig.slice.GetInstanceID())
                    {
                        foundSliceConfig = true;
                    }
                }
                if (!foundSliceConfig)
                {
                    sliceConfigToRemove.Add(sliceConfig);
                }
            }
            return sliceConfigToRemove;
        }
#if UNITY_EDITOR
        // Gets value from SerializedProperty - even if value is nested
        public static object GetValue(this UnityEditor.SerializedProperty property)
        {
            object obj = property.serializedObject.targetObject;

            FieldInfo field = null;
            foreach (var path in property.propertyPath.Split('.'))
            {
                var type = obj.GetType();
                field = type.GetField(path);
                obj = field.GetValue(obj);
            }

            return obj;
        }

        // Sets value from SerializedProperty - even if value is nested
        public static void SetValue(this UnityEditor.SerializedProperty property, object val)
        {
            object obj = property.serializedObject.targetObject;

            List<System.Collections.Generic.KeyValuePair<FieldInfo, object>> list =
                new List<System.Collections.Generic.KeyValuePair<FieldInfo, object>>();

            FieldInfo field = null;
            foreach (var path in property.propertyPath.Split('.'))
            {
                var type = obj.GetType();
                field = type.GetField(path);
                list.Add(new System.Collections.Generic.KeyValuePair<FieldInfo, object>(field, obj));
                obj = field.GetValue(obj);
            }

            // Now set values of all objects, from child to parent
            for (int i = list.Count - 1; i >= 0; --i)
            {
                list[i].Key.SetValue(list[i].Value, val);
                // New 'val' object will be parent of current 'val' object
                val = list[i].Value;
            }
        }
#endif // UNITY_EDITOR    
    }

    [System.Serializable]
    public struct KeyValuePair<T, U>
    {
        public T key;
        public U value;

        public KeyValuePair(T key, U value)
        {
            this.key = key;
            this.value = value;
        }
    }
}