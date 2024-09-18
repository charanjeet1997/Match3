using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ModulerUISystem
{
    public class TransformRotate : Animatable
    {
        
        public Vector3 currentRotation;
        public Vector3 targetRotation;
        public Vector3 finalRotation;
        public override void OnAnimationStarted()
        {
            base.OnAnimationStarted();
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
        public override void OnAnimationRunning(float animPerc)
        {
            base.OnAnimationRunning(animPerc);
            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(currentRotation), Quaternion.Euler(targetRotation),animPerc);
            Debug.Log("Rotate running");
        }
        public override void OnAnimationEnded()
        {
            base.OnAnimationEnded();
            transform.localRotation = Quaternion.Euler(finalRotation);
        }
    }
}

