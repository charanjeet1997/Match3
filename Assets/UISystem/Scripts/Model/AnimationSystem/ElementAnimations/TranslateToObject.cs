using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ModulerUISystem
{
    public class TranslateToObject : Animatable
    {
        public UnityEvent onAnimationStarted;
        public UnityEvent onAnimationEnded;

        public Transform startTransform;
        public Transform endTransform;
        public override void Awake()
        {
            base.Awake();
        }

        public override void OnAnimationStarted()
        {
            rectTransform.position = startTransform.position;
            base.OnAnimationStarted();
            onAnimationStarted?.Invoke();
        }

        public override void OnAnimationEnded()
        {
            onAnimationEnded?.Invoke();
            base.OnAnimationEnded();
        }

        public override void OnAnimationRunning(float percentage)
        {
            base.OnAnimationRunning(percentage);
            rectTransform.position= Vector2.Lerp(startTransform.position, endTransform.position, percentage);
        }
    }
}

