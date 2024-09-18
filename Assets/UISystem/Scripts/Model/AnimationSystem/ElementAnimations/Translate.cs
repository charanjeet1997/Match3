using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ModulerUISystem
{
    public class Translate : Animatable
    {
        public Vector3 fromPosition;
        public Vector3 toPosition;
        public Vector3 finalPosition;
        public override void OnAnimationStarted()
        {
            base.OnAnimationStarted();
            rectTransform.anchoredPosition = fromPosition;
        }
        public override void OnAnimationRunning(float animPerc)
        {
            base.OnAnimationRunning(animPerc);
            rectTransform.anchoredPosition = Vector2.LerpUnclamped(fromPosition, toPosition, animPerc);
        }
        public override void OnAnimationEnded()
        {
            base.OnAnimationEnded();
            rectTransform.anchoredPosition = finalPosition;
        }
    }
}

