using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputManager
{
    public abstract class InputActionMapWrapper : ScriptableObject
    {
        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] string actionMapName;
        private InputActionMap _actionMap;
        public virtual void Initialize()
        {
            _actionMap = _inputActionAsset.FindActionMap(actionMapName);
        }
        public virtual void Activate()
        {
            _actionMap.Enable();
            ToggleInputActions(true);
        }
        public virtual void Deactivate()
        {
            _actionMap.Disable();
            ToggleInputActions(false);
        }
        private void ToggleInputActions(bool status)
        {
            if (status)
            {
                foreach (var inputActions in _actionMap.actions)
                {
                    inputActions.Enable();
                }
            }
            else
            {
                foreach (var inputActions in _actionMap.actions)
                {
                    inputActions.Disable();
                }
            }
        }
    }
}