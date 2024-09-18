namespace ModulerUISystem.UI.Components
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class SliderData : MonoBehaviour
    {
        [SerializeField] private Image progressImage;
        [SerializeField] private Image knob;
        private RectTransform rectTransform;
        private RectTransform sliderStart;
        private RectTransform sliderStop;
        private GameObject start, stop;

        public Image ProgressImage
        {
            get => progressImage;
            set => progressImage = value;
        }

        public RectTransform SliderStart
        {
            get => sliderStart;
            set => sliderStart = value;
        }

        public RectTransform SliderStop
        {
            get => sliderStop;
            set => sliderStop = value;
        }

        public Image Knob
        {
            get => knob;
            set => knob = value;
        }

        public Vector2 KnobPosition
        {
            get => knob.rectTransform.position;
            set => knob.rectTransform.position = value;
        }

        private void Awake()
        {
            rectTransform = progressImage.GetComponent<RectTransform>();
            start = new GameObject("Start");
            stop = new GameObject("Stop");
            start.transform.parent = stop.transform.parent = rectTransform.transform;
            sliderStart = start.AddComponent<RectTransform>();
            sliderStop = stop.AddComponent<RectTransform>();
            sliderStart.position = sliderStop.position = rectTransform.position;
            sliderStart.sizeDelta = sliderStop.sizeDelta = new Vector2(0.1f, 10);
            sliderStart.anchorMin = new Vector2(0f, 0);
            sliderStart.anchorMax = new Vector2(0f, 1);
            sliderStop.anchorMin = new Vector2(1f, 0);
            sliderStop.anchorMax = new Vector2(1f, 1);
        }
    }
}