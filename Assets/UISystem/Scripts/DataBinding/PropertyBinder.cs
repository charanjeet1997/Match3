using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataBinding.Core;
using UnityEngine;

public class PropertyBinder : MonoBehaviour
{
    public MonoBehaviour source;
    public MonoBehaviour destination;
    [ContextMenu("FindFieldValue")]
    public void FindFieldValue()
    {
        FieldInfo[] fields = source.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(info => info.FieldType.BaseType.Equals(typeof(Property))).ToArray();
        // FieldInfo[] fields = source.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
        MemberInfo[] memberInfos = source.GetType().GetMembers(BindingFlags.Instance |BindingFlags.Public).Where(info => info.MemberType.Equals(MemberTypes.Property)).ToArray();

        // foreach (var memberInfo in memberInfos)
        // {
        //     Debug.Log(memberInfo.Name);
        // }
        
        
        foreach (var field in fields)
        {
            Property p = (Property) field.GetValue(source);
            // Debug.Log(field.ToString());
            

            
            Debug.Log(p.Value.GetType());
            // Debug.Log(field.FieldType.IsGenericType);
            Debug.Log(field.FieldType.BaseType.Equals(typeof(Property)));
            // Debug.Log(p);
        }
    }
}
