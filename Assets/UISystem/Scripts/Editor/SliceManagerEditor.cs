namespace ModulerUISystem.Editor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Rendering;
    using UnityEngine;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(SliceManager))]
    public class SliceManagerEditor : Editor
    {
        #region PUBLIC_VARS

        private SliceManager sliceManger;
        private SerializedObject so;
        private SerializedProperty propCanvasSortingOrderManager;
        private SerializedProperty propCameraStackManager;


        #endregion

        #region PRIVATE_VARS

        #endregion

        #region UNITY_CALLBACKS

        private void OnEnable()
        {
            sliceManger = (SliceManager) target;
            so = serializedObject;
            propCanvasSortingOrderManager = so.FindProperty("_canvasSortingOrderManager");
            propCameraStackManager = so.FindProperty("_cameraStackManager");

        }

        public override void OnInspectorGUI()
        {
            so.Update();
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PropertyField(propCanvasSortingOrderManager);
                if (GUILayout.Button("Self"))
                {
                    CacheCanvasSortingOrderManager();
                }
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PropertyField(propCameraStackManager);
                if (GUILayout.Button("Self"))
                {
                    CacheCameraManager();
                }
            }

            so.ApplyModifiedProperties();
        }

        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS

        public void CacheCanvasSortingOrderManager()
        {
            sliceManger._canvasSortingOrderManager = sliceManger.GetComponent<AbstractCanvasSortingOrderManager>();
        }

        public void CacheCameraManager()
        {
            sliceManger._cameraStackManager = sliceManger.GetComponent<AbstractCameraStackManager>();
        }

        #endregion
    }
}