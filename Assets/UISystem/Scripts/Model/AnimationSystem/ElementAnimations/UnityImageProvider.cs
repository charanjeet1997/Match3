namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    [RequireComponent(typeof(Image))]
    public class UnityImageProvider : ImageProvider
    {
        [SerializeField] private Image image;
        public override void OnColorUpdated(Color color)
        {
            base.OnColorUpdated(color);
            this.image.color = color;
        }
    }
}