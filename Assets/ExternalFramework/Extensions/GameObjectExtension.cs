/// <summary>
/// This Class Helps to give extra functionality to GameObject
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{
    public static T[] FindGameObjectOfTypeWithTagUnderChild<T>(this GameObject gameObject, string tag)
    {
        List<T> ts = new List<T>();
        Transform[] _transforms;
        _transforms = gameObject.GetComponentsInChildren<Transform>();
        foreach (var transform in _transforms)
        {
            if (transform.gameObject.tag == tag)
            {
                ts.Add(transform.GetComponent<T>());
            }
        }

        return ts.ToArray();
    }

    public static void SafeDestroy(this GameObject go)
    {
        if (go != null)
        {
            MonoBehaviour.Destroy(go);
        }
        else
        {
            Debug.Log("The object that you're trying to destroy doesn't exist!");
        }
    }

    public static void Deactivate(this GameObject go)
    {
        if (go != null)
        {
            go.SetActive(false);
        }
        else
        {
            Debug.Log("The object that you're trying to deactivate doesn't exist!");
        }
    }

    public static void Activate(this GameObject go)
    {
        if (go != null)
        {
            go.SetActive(true);
        }
        else
        {
            Debug.Log("The object that you're trying to activate doesn't exist!");
        }
    }

    public static T FindComponentInParent<T>(this GameObject gameObject)
    {
        Transform[] transforms;
        transforms = gameObject.GetComponentsInParent<Transform>();
        foreach (var t in transforms)
        {
            var val = t.GetComponent<T>();
            if (val != null)
                return val;
        }

        return gameObject.GetComponent<T>();
    }
    
    public static void RemoveComponent<Component>(this GameObject obj, bool immediate = false)
    {
        Component component = obj.GetComponent<Component>();

        if (component != null)
        {
            if (immediate)
            {
                Object.DestroyImmediate(component as Object, true);
            }
            else
            {
                Object.Destroy(component as Object);
            }
        }
    }
}