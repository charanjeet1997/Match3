namespace ModulerUISystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class UIToggle : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private bool isToggle;
        [SerializeField] private ToggleEvent onToggleValueChange;
        [SerializeField] private TMP_Text toggleText;
        [SerializeField] private Image toggleImage;
        [SerializeField] private Image hitBox;

        public TMP_Text ToggleText
        {
            get => toggleText;
            private set => toggleText = value;
        }

        public Image ToggleImage
        {
            get => toggleImage;
            set => toggleImage = value;
        }

        public bool IsToggle
        {
            get => isToggle;
            set => isToggle = value;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (hitBox.raycastTarget)
            {
                IsToggle = !IsToggle;
                onToggleValueChange?.Invoke(isToggle);
                SetToggleStatus(IsToggle);
            }
        }

        public void Init()
        {
            hitBox = transform.GetChild(0).GetComponent<Image>();
            Transform child = transform.GetChild(1);
            toggleText = child.GetComponentInChildren<TMP_Text>();
            toggleImage = child.transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>();
        }

        private void SetToggleStatus(bool status)
        {
            toggleImage.enabled = status;
        }

        private void OnValidate()
        {
            Init();
            // Debug.Log("UIToggle OnValidate Called");
        }

        [Serializable]
        partial class ToggleEvent : UnityEvent<bool>
        {
        }
    }
}