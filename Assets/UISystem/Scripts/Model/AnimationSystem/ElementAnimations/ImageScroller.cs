using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ModulerUISystem
{
    [RequireComponent(typeof(Image))]
    public class ModulerUISystem : Animatable
    {
        private Image image;
        private Material material;
        Vector2 scroll;
        public Vector2 speed = new Vector2(0.01f, 0f);
        public override void Awake()
        {
            base.Awake();
            image = GetComponent<Image>();
            material = image.material;
            material.mainTextureOffset=Vector2.zero;
        }
        public override void OnAnimationStarted()
        {
            base.OnAnimationStarted();
        }
        public override void OnAnimationRunning(float animPerc)
        {
            base.OnAnimationRunning(animPerc);
            material.mainTextureOffset += Time.fixedDeltaTime*speed;
        }
        public override void OnAnimationEnded()
        {
            base.OnAnimationEnded();
        }
    }
}

