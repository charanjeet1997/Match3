using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ModulerUISystem.UI.Components
{
    public class UIRadioButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private int radioButtonindex;
        private Image hitBox;
        [SerializeField] private bool isSelected;
        [SerializeField]private UIRadioGroup radioGroup;
        private TMP_Text radioText;
        private Image radioImageHolder;

        public int RadioButtonindex
        {
            get => radioButtonindex;
            set => radioButtonindex = value;
        }

        public bool IsSelected
        {
            get => isSelected;
            set => isSelected = value;
        }

        public UIRadioGroup RadioGroup
        {
            get => radioGroup;
            set => radioGroup = value;
        }

        public TMP_Text RadioText
        {
            get => radioText;
            set => radioText = value;
        }

        public Image RadioImageHolder
        {
            get => radioImageHolder;
            set => radioImageHolder = value;
        }

        public void Init()
        {
            hitBox = transform.GetChild(0).GetComponent<Image>();
            Transform child = transform.GetChild(1);
            radioText = child.GetComponentInChildren<TMP_Text>();
            radioImageHolder = child.GetComponentInChildren<Image>();
            radioText.text = "Radio ";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (hitBox.raycastTarget)
            {
                IsSelected = !IsSelected;
                if (isSelected)
                {
                    Debug.Log(radioText.text+" "+radioButtonindex);
                    radioGroup.onRadioSelected?.Invoke(radioText.text);
                }
                radioGroup.SelectedRadioButtonIndex = radioButtonindex;
            }
        }

       

        private void OnValidate()
        {
            Init();
        }
    }
}