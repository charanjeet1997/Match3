namespace ModulerUISystem.Editor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Rendering;
    using UnityEngine;

    [CanEditMultipleObjects]
    [CustomPropertyDrawer(typeof(SliceConfig))]
    public class SliceConfigEditor : PropertyDrawer
    {
        #region PUBLIC_VARS

        private int lineCount=5;
        #endregion

        #region PRIVATE_VARS

        #endregion

        #region UNITY_CALLBACKS

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position ,label, property);
            CalculateLineCount(property);
            // position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            // Drawing slice config
            float lastY = position.y;
            Rect sliceRect = new Rect(position.x,lastY,position.width,EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(sliceRect, property.FindPropertyRelative("slice"));
            if (property.FindPropertyRelative("slice").objectReferenceValue!=null)
            {
                // property.FindPropertyRelative("sliceName").stringValue =
                //     property.FindPropertyRelative("slice").objectReferenceValue.name;
            }
            
            //Drawing Overriden transition
            lastY += EditorGUIUtility.singleLineHeight;
            Rect overrideShowTransitionRect = new Rect(position.x,position.y+EditorGUIUtility.singleLineHeight,position.width,EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(overrideShowTransitionRect, property.FindPropertyRelative("overrideViewShowTransition"));

            if (property.FindPropertyRelative("overrideViewShowTransition").boolValue)
            {
                //Draw 
                lastY += EditorGUIUtility.singleLineHeight;
                Rect showTransitionConfigRect = new Rect(position.x,lastY,position.width,EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(showTransitionConfigRect, property.FindPropertyRelative("showTransitionConfig"));
            }
            
            //Drawing Overriden transition
            lastY += EditorGUIUtility.singleLineHeight;
            Rect overrideHideTransitionRect = new Rect(position.x,lastY,position.width,EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(overrideHideTransitionRect, property.FindPropertyRelative("overrideViewHideTransition"));
            
            if (property.FindPropertyRelative("overrideViewHideTransition").boolValue)
            {
                //Draw hide transition config
                lastY += EditorGUIUtility.singleLineHeight;
                Rect hideTransitionConfigRect = new Rect(position.x,lastY,position.width,EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(hideTransitionConfigRect, property.FindPropertyRelative("hideTransitionConfig"));
            }
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * CalculateLineCount(property) + EditorGUIUtility.standardVerticalSpacing * (CalculateLineCount(property)-1);
        }
        
        #endregion

        #region PRIVATE_METHODS

        private int CalculateLineCount(SerializedProperty property)
        {
            lineCount = 3;
            if (property.FindPropertyRelative("overrideViewShowTransition").boolValue)
            {
                lineCount++;
            }
            if (property.FindPropertyRelative("overrideViewHideTransition").boolValue)
            {
                lineCount++;
            }

            return lineCount;
        }
        #endregion

        #region PUBLIC_METHODS
        #endregion
    }
}