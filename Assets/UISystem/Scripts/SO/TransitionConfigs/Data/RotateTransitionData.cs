
namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    public class RotationTransition : Transition
    {
        public float targetRotation;
        public AnimationCurve curve;

        public RotationTransition(float duration, SlicePanel[] slicePanels, float targetRotation, AnimationCurve curve) : base(duration, slicePanels)
        {
            this.targetRotation = targetRotation;
            this.curve = curve;
        }

        public override void OnAnimationRunning(float percentage)
        {
            for (int indexOfSlice = 0; indexOfSlice < slicePanels.Length; indexOfSlice++)
            {
                slicePanels[indexOfSlice].rectTransform.localRotation = Quaternion.Euler(Vector3.forward * (targetRotation * curve.Evaluate(percentage)));
            }
        }

        public override void OnAnimationEnded()
        {
            for (int indexOfSlice = 0; indexOfSlice < slicePanels.Length; indexOfSlice++)
            {
                slicePanels[indexOfSlice].rectTransform.localRotation =
                    Quaternion.Euler(Vector3.forward * targetRotation);
            }
        }

        public override void OnAnimationStarted()
        {
            
        }
    }
}