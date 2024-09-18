using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModulerUISystem
{
    [RequireComponent(typeof(ImageProvider))]

    public class ColorTransition : Animatable
    {
        private ImageProvider imageProvider;
        public Gradient transitionColor;
        public override void Awake()
        {
            base.Awake();
            imageProvider = GetComponent<ImageProvider>();
        }

        public override void OnAnimationStarted()
        {
            base.OnAnimationStarted();
        }
        public override void OnAnimationRunning(float animPerc)
        {
            base.OnAnimationRunning(animPerc);
            imageProvider.Color = transitionColor.Evaluate(animPerc);
        }
        public override void OnAnimationEnded()
        {
            base.OnAnimationEnded();
        }
    }
}

