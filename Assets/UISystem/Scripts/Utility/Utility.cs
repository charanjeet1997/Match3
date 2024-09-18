
namespace ModulerUISystem
{
    using System.Collections;
using System.Linq;
using System.Reflection;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    public static class Utility
    {
        #region PUBLIC_METHODS
        public static void DoActionAfterSomeTime(this MonoBehaviour monoBehaviour, float duration,params Action[] actions)
        {
            monoBehaviour.StartCoroutine(DoActionRoutine(duration, actions));
        }
        public static void ExecuteActions(params Action[] actions)
        {
            if(actions==null)
                return;
            for (int indexOfAction = 0; indexOfAction < actions.Length; indexOfAction++)
            {
                actions[indexOfAction]();
            }
        }
        #endregion

        #region PRIVATE_METHODS
        static IEnumerator DoActionRoutine(float duration,params Action[] actions)
        {
            yield return new WaitForSeconds(duration);
            ExecuteActions(actions);   
        }
        #endregion
    }
}