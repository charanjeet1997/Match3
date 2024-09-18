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

namespace ModulerUISystem
{
    public class UIButton : MonoBehaviour, IPointerClickHandler
    {
        private Image hitBox;
        public UnityEvent onButtonClick;
        public delegate void OnClick();
        private bool isClicked = false;
        private TMP_Text buttonText;
        private Image buttonImage;

        public TMP_Text ButtonText
        {
            get => buttonText;
            set => buttonText = value;
        }

        public Image ButtonImage
        {
            get => buttonImage;
            set => buttonImage = value;
        }
        [ContextMenu("Cache")]
        public void Init()
        {
            hitBox = transform.GetChild(0).GetComponent<Image>();
            Transform child = transform.GetChild(1);
            buttonImage = child.GetComponentInChildren<Image>();
            buttonText = child.GetComponentInChildren<TMP_Text>();
        }

        public void AddListener(OnClick onClick)
        {
            onButtonClick.AddListener(() => onClick());
        }

        public void RemoveListener(OnClick onClick)
        {
            onButtonClick.RemoveListener(() => onClick());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (hitBox.raycastTarget)
            {
                onButtonClick?.Invoke();
            }
        }

        private void Awake()
        {
            Init();
        }

        public void PrintSomething(string message)
        {
            buttonText.text = message;
            Debug.Log(message);
        }

        private void OnValidate()
        {
            Init();
        }
    }
}