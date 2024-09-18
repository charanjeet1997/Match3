using System;
using UnityEngine.UI;

namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class NormalizePositionController : MonoBehaviour
    {
        private RectTransform rectTransform;
        private CanvasScaler canvasScaler;
        
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasScaler = gameObject.FindComponentInParent<CanvasScaler>();
        }

        public void OnPropogationReceiveStart(Vector2 val)
        {
            rectTransform.anchoredPosition = canvasScaler.referenceResolution * val;
        }

        public void OnPropogationReceive(Vector2 val)
        {
            Debug.Log(rectTransform.sizeDelta.ToString());
            rectTransform.anchoredPosition = canvasScaler.referenceResolution * val;
        }

        public void OnPropogationReceiveEnd(Vector2 val)
        {
            Debug.Log(rectTransform.sizeDelta.ToString());

            rectTransform.anchoredPosition = canvasScaler.referenceResolution * val;
        }
    }
}