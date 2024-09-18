using System;

namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    [RequireComponent(typeof(CanvasGroup))]
    public class SlicePanel : MonoBehaviour
    {
        #region PUBLIC_VARS
        [HideInInspector]
        public RectTransform rectTransform;
        [HideInInspector]
        public CanvasGroup canvasGroup;
        public int associatedAnimationTarget;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS
        private void Awake()
        {
            Initialize();
        }

        private void Reset()
        {
            Initialize();
        }

        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS
        public void Initialize()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }
        #endregion
    }
}