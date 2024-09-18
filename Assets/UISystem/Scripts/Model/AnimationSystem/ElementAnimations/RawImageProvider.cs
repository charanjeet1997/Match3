namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    [RequireComponent(typeof(RawImage))]
    public class RawImageProvider : ImageProvider
    {
        [SerializeField] private RawImage image;
        public override void OnColorUpdated(Color color)
        {
            base.OnColorUpdated(color);
            this.image.color = color;
        }
    }
}