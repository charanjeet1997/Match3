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
    public class UICustomInputField : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI errorLable;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Animator animator;
        [SerializeField] private InputValidator inputValidator;

        public string text
        {
            get => inputField.text;
            set => inputField.text = value;
        }

        public void OnSelect()
        {
            animator.ResetAllTriggers();
            animator.SetTrigger("Selected");
        }

        public void OnDeselect()
        {
            animator.ResetAllTriggers();
            if (inputField.text == string.Empty || inputField.text == null)
            {
                animator.SetTrigger("Normal");
            }
            else
            {
                animator.SetTrigger("Disabled");
            }
        }

        public bool Validate()
        {
            animator.ResetAllTriggers();
            if (inputValidator.Validate(inputField.text))
            {
                animator.SetTrigger("Disabled");
                return true;
            }
            else
            {
                inputField.DeactivateInputField(true);
                errorLable.text = inputValidator.ErrorDescription;
                animator.SetTrigger("Error");
                return false;
            }
        }

        public void OnCloseButtonClicked()
        {
            inputField.text = string.Empty;
            animator.ResetAllTriggers();
            CoroutineExtension.ExecuteAfterFrame(this, () => inputField.ActivateInputField());
        }
    }
}