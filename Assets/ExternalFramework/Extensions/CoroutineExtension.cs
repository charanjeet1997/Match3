/// <summary>
/// This Class Helps to give extra functionality to Coroutine 
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using Games.UnnamedArcade3d.Entities.LittleRed;
using UnityEngine;
using Object = UnityEngine.Object;

public static class CoroutineExtension
{
    public static Coroutine Execute(this MonoBehaviour monoBehaviour, Action action, float time)
    {
        return monoBehaviour.StartCoroutine(DelayedAction(action, time));
    }

    public static IEnumerator DelayedAction(Action action, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        if(action!=null)
            action();
    }

    public static Coroutine Execute(this MonoBehaviour monoBehaviour, float time, params Action[] action)
    {
        return monoBehaviour.StartCoroutine(DelayedAction(action, time));
    }

    public static Coroutine ExecuteAfterFrame(this MonoBehaviour monoBehaviour, params Action[] action)
    {
        return monoBehaviour.StartCoroutine(DoActionFrame(action));
    }

    public static IEnumerator DoActionFrame(Action[] action)
    {
        yield return new WaitForEndOfFrame();
        for (int indexOfAction = 0; indexOfAction < action.Length; indexOfAction++)
        {
            if (action[indexOfAction] != null)
            {
                action[indexOfAction]();
            }
        }
    }

    static IEnumerator DelayedAction(Action[] action, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        for (int indexOfAction = 0; indexOfAction < action.Length; indexOfAction++)
        {
            if (action[indexOfAction] != null)
            {
                action[indexOfAction]();
            }
        }
    }


    public static Coroutine WaitUntil(this MonoBehaviour monoBehaviour, System.Func<bool> condition, Action action)
    {
        return monoBehaviour.StartCoroutine(DelayedAction(condition, action));
    }

    static IEnumerator DelayedAction(System.Func<bool> condition, Action action)
    {
        if (condition == null)
            yield break;
        yield return new WaitUntil(condition);
        if(action!=null)
            action();
    }

    public static Coroutine InstantiateAndDestroyObjectAfterSometime(this MonoBehaviour monoBehaviour,
        GameObject _prefab, Vector3 position, Quaternion rotation, Transform parent, float destructionTime,
        Action onDestroyed)
    {
        return monoBehaviour.StartCoroutine(InstantiateAndDestroyGameObjectAfterSometime(monoBehaviour, _prefab,
            position, rotation, parent, destructionTime, onDestroyed));
    }

    static IEnumerator InstantiateAndDestroyGameObjectAfterSometime(this MonoBehaviour monoBehaviour,
        GameObject _prefab, Vector3 position, Quaternion rotation, Transform parent, float actionWaitTime,
        Action onDestroyed)
    {
        GameObject instantiatedObject = UnityEngine.Object.Instantiate(_prefab, position, rotation, parent);
        yield return new WaitForSeconds(actionWaitTime);
        UnityEngine.Object.Destroy(instantiatedObject);
        if (onDestroyed != null)
            onDestroyed();
    }


    static IEnumerator InstantiateAfterSometime(float time)
    {
        yield return new WaitForSeconds(time);
    }
}