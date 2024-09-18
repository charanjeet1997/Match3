using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ModulerUISystem
{
    [RequireComponent(typeof(Image))]
    public class Fade : Animatable
    {
        public float fromAlpha;
        public float toAlpha;
        public float finalAlpha;
        Image image;
        public override void Awake()
        {
            base.Awake();
            image = GetComponent<Image>();
        }
        public override void OnAnimationStarted()
        {
            base.OnAnimationStarted();
            image.color = image.color.WithAlpha(fromAlpha);
        }
        public override void OnAnimationRunning(float animPerc)
        {
            base.OnAnimationRunning(animPerc);
            image.color = Color.LerpUnclamped(image.color.WithAlpha(fromAlpha), image.color.WithAlpha(toAlpha), animPerc);
        }
        public override void OnAnimationEnded()
        {
            base.OnAnimationEnded();
            image.color = image.color.WithAlpha(finalAlpha);
        }
    }
}

