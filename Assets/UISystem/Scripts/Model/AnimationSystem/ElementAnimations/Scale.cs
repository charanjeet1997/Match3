using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModulerUISystem
{
    public class Scale : Animatable
    {
        public Vector3 fromScale;
        public Vector3 toScale;
        public Vector3 finalScale;
        private Transform Transform {
            get
            {
                if (_transform == null)
                {
                    _transform = transform;
                }

                return _transform;
            }
        }

        private Transform _transform;
        



        public override void OnAnimationStarted()
        {
            base.OnAnimationStarted();
            Transform.localScale = fromScale;
        }

        public override void OnAnimationRunning(float animPerc)
        {
            base.OnAnimationRunning(animPerc);
            Transform.localScale = Vector3.LerpUnclamped(fromScale, toScale, animPerc);
        }

        public override void OnAnimationEnded()
        {
            base.OnAnimationEnded();
            Transform.localScale = finalScale;
        }
    }
}