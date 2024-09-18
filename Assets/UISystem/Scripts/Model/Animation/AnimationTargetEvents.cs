namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class AnimationTargetEvents : MonoBehaviour
    {
        public void OnAnimationStarted()
        {
            Debug.Log("OnAnimationStarted");
        }
        public void OnAnimationEnded()
        {
            Debug.Log("OnAnimationEnded");

        }
        public void OnAnimationRunning()
        {
            Debug.Log("OnAnimationRunning");
        }
    }
}