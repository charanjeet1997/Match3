namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class VectorRangeAttribute : PropertyAttribute
    {
        public float min, max;
        public bool shouldDraw;
        public VectorRangeAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}