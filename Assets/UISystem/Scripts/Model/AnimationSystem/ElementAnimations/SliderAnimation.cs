using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace ModulerUISystem
{
    public class SliderAnimation : Animatable
    {
        [SerializeField] private Slider _slider;
        public override void Awake()
        {
            base.Awake();
            
        }
        public override void OnAnimationStarted()
        {
            base.OnAnimationStarted();
        }
        public override void OnAnimationEnded()
        {
            base.OnAnimationEnded();
        }
        public override void OnAnimationRunning(float percentage)
        {
            base.OnAnimationRunning(percentage);
            _slider.value = percentage;
        }
    }
}
