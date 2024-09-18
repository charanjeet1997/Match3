using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.Events;

namespace ModulerUISystem.UI.Components
{
    public class UIRadioGroup : MonoBehaviour
    {
        public int numberOfRadioButtons;
        public RadioEvent onRadioSelected;
        [HideInInspector] public string[] radioLabel = new string[0];
        public UIRadioButton[] radioButtons = new UIRadioButton[0];
        [SerializeField] private UIRadioButton radioButtonPrefab;
        [SerializeField] private GameObject radioParent;
        private int selectedRadioButtonindex;

        public int SelectedRadioButtonIndex
        {
            get => selectedRadioButtonindex;
            set => selectedRadioButtonindex = value;
        }
        
        private void Awake()
        {
            radioButtons = GetComponentsInChildren<UIRadioButton>();
        }

        private void Update()
        {
            for (int indexOfRadioButtons = 0; indexOfRadioButtons < radioButtons.Length; indexOfRadioButtons++)
            {
                if (selectedRadioButtonindex != radioButtons[indexOfRadioButtons].RadioButtonindex)
                {
                    radioButtons[indexOfRadioButtons].IsSelected = false;
                }
            }
        }

        public UIRadioButton GetSelectedRadioButton()
        {
            return radioButtons[selectedRadioButtonindex];
        }

        [Serializable]
        public class RadioEvent : UnityEvent<string>
        {
        }

    }
}