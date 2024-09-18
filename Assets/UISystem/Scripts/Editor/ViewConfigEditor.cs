namespace ModulerUISystem.Editor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Rendering;
    using UnityEngine;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(ViewConfig))]
    public class ViewConfigEditor : Editor
    {
        #region PUBLIC_VARS
        private ViewConfig _viewConfig;
        private SerializedObject so;
        private SerializedProperty propHidePrevious;
        private SerializedProperty propBlockInteractionDuringTransition;
        private SerializedProperty propShowTransitionConfig;
        private SerializedProperty propHideTransitionConfig;
        private SerializedProperty propSliceConfigList;
        #endregion

        #region PRIVATE_VARS

        #endregion

        #region UNITY_CALLBACKS

        private void OnEnable()
        {
            _viewConfig = (ViewConfig) target;
            so = serializedObject;
            propHidePrevious = so.FindProperty("hidePrevious");
            propBlockInteractionDuringTransition = so.FindProperty("blockInteractionDuringTransition");
            propShowTransitionConfig = so.FindProperty("showTransitionConfig");
            propHideTransitionConfig = so.FindProperty("hideTransitionConfig");
            propSliceConfigList = so.FindProperty("sliceConfigList");
        }

        public override void OnInspectorGUI()
        {
            so.Update();
            EditorGUILayout.PropertyField(propHidePrevious);
            EditorGUILayout.PropertyField(propBlockInteractionDuringTransition);
            EditorGUILayout.PropertyField(propShowTransitionConfig);
            EditorGUILayout.PropertyField(propHideTransitionConfig);

            EditorGUILayout.Space(10);
            if (IsSlicesAvailableInHeirarchy())
            {
                EditorGUILayout.HelpBox(new GUIContent(
                    "You must remove all slices from your hierarchy in order to instantiate slice through a ViewConfig. Remember to save your changes if you have any."));
            }
            else
            {
                if (GUILayout.Button("Instantiate Associated Slices"))
                {
                    InstantiateAssociateSlices();
                }
            }

            EditorGUILayout.PropertyField(propSliceConfigList);

            so.ApplyModifiedProperties();
        }

        #endregion

        #region PRIVATE_METHODS

        public void InstantiateAssociateSlices()
        {
            //Find the slice manager
            UIManager.instance.ShowView(_viewConfig);
        }

        public bool IsSlicesAvailableInHeirarchy()
        {

            Slice[] slices = GameObject.FindObjectsOfType<Slice>();

            foreach (var slice in slices)
            {
                foreach (var sliceConfig in _viewConfig.sliceConfigList)
                {
                    if (sliceConfig.slice != null && sliceConfig.slice.name == slice.name)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #region PUBLIC_METHODS
        #endregion
    }
}