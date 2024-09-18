using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace ModulerUISystem.UI.Components
{
    public class UISlider : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        [Range(0, 1), SerializeField] private float progress;
        private float currentPorgress = 0;
        [SerializeField] private ProgressEvent onSliderValueChanged;
        private SliderData sliderData;
        private Image sliderImage;
        private bool isMouseDown;
        private Vector2 knobPosition,startPos,stopPos;
        public float Progress
        {
            get => progress;
            set => progress = value;
        }

        public Image SliderImage
        {
            get => sliderImage;
            set => sliderImage = value;
        }

        public SliderData SliderData
        {
            get => sliderData;
            set => sliderData = value;
        }
        [Serializable]
        partial class ProgressEvent : UnityEvent<float> { }

        private void OnProgressChangedInvoke(float progress)
        {
            onSliderValueChanged?.Invoke(progress);
        }

        private void Awake()
        {
            Init();
        }

        private void Update()
        {
            Vector3 mousePosition = Input.mousePosition;
            startPos = sliderData.SliderStart.position;
            stopPos = sliderData.SliderStop.position;
            if (isMouseDown)
            {
                knobPosition = sliderData.KnobPosition;
                progress = Mathf.Clamp01(InverseLerp(startPos, stopPos, mousePosition));
                knobPosition = Vector2.Lerp(startPos,stopPos,progress);
                sliderData.ProgressImage.fillOrigin = 0;
                OnProgressChangedInvoke(progress);
            }
            else
            {
                knobPosition = Vector2.Lerp(startPos, stopPos, progress);
            }
            sliderData.KnobPosition = knobPosition;
            sliderData.ProgressImage.fillAmount = progress;
        }

        public void Init()
        {
            sliderData = GetComponentInChildren<SliderData>();
            Transform child = transform.GetChild(1);
            sliderImage = child.GetComponentInChildren<Image>();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            isMouseDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isMouseDown = false;
        }
        
        public float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
        {
            Vector3 AB = b - a;
            Vector3 AV = value - a;
            return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
        }
    }
}