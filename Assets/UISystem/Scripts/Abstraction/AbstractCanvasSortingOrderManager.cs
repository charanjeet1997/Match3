namespace ModulerUISystem
{
    
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class AbstractCanvasSortingOrderManager : MonoBehaviour
    {
        public abstract void AddCanvases(List<Canvas> canvases);
        public abstract void RemoveCanvases(List<Canvas> canvases);
    }

}