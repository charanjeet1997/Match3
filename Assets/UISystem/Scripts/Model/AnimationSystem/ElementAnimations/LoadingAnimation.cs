using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModulerUISystem
{
    
    public class LoadingAnimation : Animatable
    {
        public Sprite normalSprite, spriteToChangeForAnimation;
        public Color normalColor, changeColor;
        [SerializeField]private Image[] imagesToAnimate;
        private Image currentImage;
        private Image previousImage;
        [SerializeField]private int imageIndex = 0;

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
            currentImage = imagesToAnimate[(imageIndex + 1) % imagesToAnimate.Length];
            previousImage = imagesToAnimate[imageIndex % imagesToAnimate.Length];
            previousImage.sprite = normalSprite;
            previousImage.color = normalColor;
            currentImage.sprite = spriteToChangeForAnimation;
            currentImage.color = changeColor;
            imageIndex= (int)(imagesToAnimate.Length*percentage);
            imageIndex++;
        }
    }
}