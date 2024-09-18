
namespace ModulerUISystem.Editor
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    using System;

    [CustomPropertyDrawer(typeof(Dropdown))]
    public class DropdownDrawer : PropertyDrawer
    {
        public string[] options = new string[0];

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Dropdown dropdown = attribute as Dropdown;
            if (property.propertyType == SerializedPropertyType.Integer)
            {
                property.intValue = EditorGUI.Popup(position,dropdown.lable,property.intValue,dropdown.items);
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use checklist with int.");
            }
        }
    }
}