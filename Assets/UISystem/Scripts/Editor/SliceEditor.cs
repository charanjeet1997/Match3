namespace ModulerUISystem.Editor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Rendering;
    using UnityEngine;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(Slice))]
    public class SliceEditor : Editor
    {
        #region PUBLIC_VARS

        private Slice slice;



        #endregion

        #region PRIVATE_VARS

        #endregion

        #region UNITY_CALLBACKS

        private void OnEnable()
        {
            slice = (Slice) target;
        }

        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();
            CacheAnimationTarget();
        }

        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS
        public void CacheAnimationTarget()
        {
            if (slice.animationTargets == null)
            {
                slice.animationTargets = slice.GetComponentInChildren<AnimationTargets>();
            }
        }
        #endregion
    }
}