using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;

namespace ModulerUISystem.Editor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Rendering;
    using UnityEngine;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(SlicePanel))]
    public class SlicePanelEditor : Editor
    {
        #region PUBLIC_VARS

        private SlicePanel slicePanel;

        #endregion

        #region PRIVATE_VARS
        private string[] displayOption = new string[]{"100","200"};
        private Slice slice;

        #endregion

        #region UNITY_CALLBACKS

        private void OnEnable()
        {
            slicePanel = (SlicePanel) target;
            slice = slicePanel.gameObject.FindComponentInParent<Slice>();
            if (slice != null)
            {
                // Debug.Log("Found Slice : "+ slice.gameObject.name);
            }
            else
            {
                // Debug.Log("Not Found Slice");                
            }
        }

        public override void OnInspectorGUI()
        {
            // base.DrawDefaultInspector();
            FindAnimationTargetOption();
            slicePanel.associatedAnimationTarget=EditorGUILayout.Popup("Associated Animation target",slicePanel.associatedAnimationTarget, displayOption);
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                EditorSceneManager.MarkSceneDirty(prefabStage.scene);
            }
        }

        #endregion

        #region PRIVATE_METHODS
        private void FindAnimationTargetOption()
        {
            if(slice==null)
                return;
            displayOption = slice.animationTargets.transform.GetChildrenName();
        }
        #endregion

        #region PUBLIC_METHODS
        
        #endregion
    }
}