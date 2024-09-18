using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModulerUISystem
{
    [RequireComponent(typeof(Image))]
    public class ImageFiller : Animatable
    {
        private Image image;
        [SerializeField] private float currentPercentage;
        [SerializeField] private float targetPercentage;
        [SerializeField] private float finalPercentage;

        public override void Awake()
        {
            base.Awake();
            image = GetComponent<Image>();
        }

        public override void OnAnimationStarted()
        {
            base.OnAnimationStarted();
            image.fillAmount = currentPercentage;
        }

        public override void OnAnimationRunning(float animPerc)
        {
            base.OnAnimationRunning(animPerc);
            image.fillAmount = targetPercentage*animPerc;
        }

        public override void OnAnimationEnded()
        {
            base.OnAnimationEnded();
            image.fillAmount = finalPercentage;
        }
    }
}