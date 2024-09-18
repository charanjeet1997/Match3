namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class ImageProvider : MonoBehaviour
    {
        public Color Color
        {
            set { OnColorUpdated(value); }
            get { return color; }
        }

        private Color color;

        public virtual void OnColorUpdated(Color color)
        {
            this.color = color;
        }
    }
}