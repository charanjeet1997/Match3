namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    public class SlideShowTransition : Transition
    {
        public Vector2 initialPosition;
        public Vector2 direction;
        public AnimationCurve curve;
        public SlideShowTransition(float duration,SlicePanel[] slicePanels,Vector2 direction,AnimationCurve curve) : base(duration,slicePanels)
        {
            this.initialPosition = new Vector2(Screen.width,Screen.height)*direction;
            this.direction = direction;
            this.curve = curve;
        }
        public override void OnAnimationRunning(float percentage)
        {
            for (int indexOfSlice = 0; indexOfSlice < slicePanels.Length; indexOfSlice++)
            {
                slicePanels[indexOfSlice].rectTransform.anchoredPosition = Vector3.LerpUnclamped(initialPosition,Vector3.zero,curve.Evaluate(percentage));
            }
        }
        public override void OnAnimationEnded()
        {
            for (int indexOfSlice = 0; indexOfSlice < slicePanels.Length; indexOfSlice++)
            {
                slicePanels[indexOfSlice].rectTransform.anchoredPosition = Vector3.zero;
            }
        }
        public override void OnAnimationStarted()
        {
            for (int indexOfSlice = 0; indexOfSlice < slicePanels.Length; indexOfSlice++)
            {
                slicePanels[indexOfSlice].rectTransform.anchoredPosition = initialPosition;
            }
        }
    }
    
    public class SlideHideTransition : Transition
    {
        public Vector2 targetPosition;
        public AnimationCurve curve;
        public SlideHideTransition(float duration,Vector2 direction,SlicePanel[] slices,AnimationCurve animationCurve) : base(duration,slices)
        {
            this.targetPosition = new Vector2(Screen.width,Screen.height)*direction;
            this.curve = animationCurve;
        }
        public override void OnAnimationRunning(float percentage)
        {
            for (int indexOfSlice = 0; indexOfSlice < slicePanels.Length; indexOfSlice++)
            {
                slicePanels[indexOfSlice].rectTransform.anchoredPosition = Vector3.LerpUnclamped(Vector3.zero, targetPosition,curve.Evaluate(percentage));
            }
        }
        public override void OnAnimationStarted()
        {
            for (int indexOfSlice = 0; indexOfSlice < slicePanels.Length; indexOfSlice++)
            {
                slicePanels[indexOfSlice].rectTransform.anchoredPosition = Vector3.zero;
            }
        }
        public override void OnAnimationEnded()
        {
            for (int indexOfSlice = 0; indexOfSlice < slicePanels.Length; indexOfSlice++)
            {
                slicePanels[indexOfSlice].rectTransform.anchoredPosition = targetPosition;
            }
        }
    }
}