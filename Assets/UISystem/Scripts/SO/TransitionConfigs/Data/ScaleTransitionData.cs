namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    public class ScaleShowTransition : Transition
    {
        
        public Vector3 targetScale;
        public AnimationCurve curve;
        public ScaleShowTransition(float duration,SlicePanel[] slicePanels,Vector3 targetScale,AnimationCurve curve) : base(duration,slicePanels)
        {
        
            this.targetScale = targetScale;
            this.curve = curve;
        }
        public override void OnAnimationRunning(float percentage)
        {
            for (int indexOfSlice = 0; indexOfSlice < slicePanels.Length; indexOfSlice++)
            {
                slicePanels[indexOfSlice].rectTransform.localScale = Vector3.LerpUnclamped(Vector3.zero, targetScale,curve.Evaluate(percentage));
            }
        }
        public override void OnAnimationEnded()
        {
            for (int indexOfSlice = 0; indexOfSlice < slicePanels.Length; indexOfSlice++)
            {
                slicePanels[indexOfSlice].rectTransform.localScale = targetScale;
            }
        }
        public override void OnAnimationStarted()
        {
            for (int indexOfSlice = 0; indexOfSlice < slicePanels.Length; indexOfSlice++)
            {
                slicePanels[indexOfSlice].rectTransform.localScale = Vector3.zero;
            }
        }
    }
    
    public class ScaleHideTransition : Transition
    {

        public Vector3 targetScale;
        public AnimationCurve curve;
        public ScaleHideTransition(float duration,Vector2 targetScale,SlicePanel[] slices,AnimationCurve animationCurve) : base(duration,slices)
        {
            this.targetScale = targetScale;
            this.curve = animationCurve;
        }
        public override void OnAnimationRunning(float percentage)
        {
            for (int indexOfSlice = 0; indexOfSlice < slicePanels.Length; indexOfSlice++)
            {
                slicePanels[indexOfSlice].rectTransform.localScale = Vector3.LerpUnclamped(Vector3.one, targetScale,curve.Evaluate(percentage));
            }
        }
        public override void OnAnimationStarted()
        {
            for (int indexOfSlice = 0; indexOfSlice < slicePanels.Length; indexOfSlice++)
            {
                slicePanels[indexOfSlice].rectTransform.localScale = Vector3.one;
            }
        }
        public override void OnAnimationEnded()
        {
            for (int indexOfSlice = 0; indexOfSlice < slicePanels.Length; indexOfSlice++)
            {
                slicePanels[indexOfSlice].rectTransform.localScale = targetScale;
            }
        }
    }
}