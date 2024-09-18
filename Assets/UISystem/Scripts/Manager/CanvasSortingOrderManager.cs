namespace ModulerUISystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Linq;

    public class CanvasSortingOrderManager : AbstractCanvasSortingOrderManager
    {
        #region PUBLIC_VARS
        private List<Canvas> canvases;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS
        public void Awake()
        {
            canvases = new List<Canvas>();
        }
        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS
        public override void AddCanvases(List<Canvas> canvases)
        {
            foreach (var canvas in canvases)
            {
                if (!this.canvases.Contains(canvas))
                {
                    this.canvases.Add(canvas);
                }
                else
                {
                    this.canvases.Remove(canvas);
                    this.canvases.Add(canvas);
                }
            }
            AdjustCanvasSortingOrder();
        }

        public override void RemoveCanvases(List<Canvas> canvases)
        {
            foreach (var canvas in canvases)
            {
                if (this.canvases.Contains(canvas))
                {
                    this.canvases.Remove(canvas);
                }
            }
            AdjustCanvasSortingOrder();
        }

        public void AdjustCanvasSortingOrder()
        {
            for (int indexOfCanvas = 0; indexOfCanvas < this.canvases.Count; indexOfCanvas++)
            {
                this.canvases[indexOfCanvas].sortingOrder = indexOfCanvas;
            }
        }
        
        #endregion
    }
}
