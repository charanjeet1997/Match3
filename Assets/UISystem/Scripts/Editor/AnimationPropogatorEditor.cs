
namespace ModulerUISystem.Editor
{
    using UnityEditor.Experimental.SceneManagement;
    using UnityEditor.SceneManagement;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEditor.Rendering;
    using UnityEngine;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(AnimationPropagator))]
    public class AnimationPropogatorEditor : Editor
    {
        #region PUBLIC_VARS
        public AnimationPropagator animationPropagator;
        #endregion

        #region PRIVATE_VARS
        private Slice slice;
        #endregion

        #region UNITY_CALLBACKS
        private void OnEnable()
        {
            animationPropagator = (AnimationPropagator) target;
            slice = animationPropagator.gameObject.FindComponentInParent<Slice>();
        }
        public override void OnInspectorGUI()
        {
            base.DrawDefaultInspector();
            animationPropagator.associatedPanels = GetAccociatedSlicePanel();
            animationPropagator.propagators = GetAttachedPropogators();
            AssignAnimator();
            AssignPropogatorMembers();
            
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                EditorSceneManager.MarkSceneDirty(prefabStage.scene);
            }
        }
        #endregion

        #region PRIVATE_METHODS

        private void AssignAnimator()
        {
            animationPropagator.animator = animationPropagator.GetComponent<Animator>();
        }
        private SlicePanel[] GetAccociatedSlicePanel()
        {
            List<SlicePanel> slicePanels = new List<SlicePanel>();
            for (int indexOfSlicePanel = 0; indexOfSlicePanel < slice.slicePanels.Length; indexOfSlicePanel++)
            {
                if (slice.slicePanels[indexOfSlicePanel].associatedAnimationTarget == animationPropagator.transform.GetSiblingIndex())
                {
                    slicePanels.Add(slice.slicePanels[indexOfSlicePanel]);
                }
            }
            return slicePanels.ToArray();
        }

        private Propogrator[] GetAttachedPropogators()
        {
            return animationPropagator.GetComponents<Propogrator>();
        }

        

        private void AssignPropogatorMembers()
        {
            for (int indexOfSlicePanel = 0; indexOfSlicePanel < animationPropagator.propagators.Length; indexOfSlicePanel++)
            {
                animationPropagator.propagators[indexOfSlicePanel].AssignMembers(animationPropagator.associatedPanels);
            }
        }
        
        
    
        
        #endregion

        #region PUBLIC_METHODS

        #endregion
    }
}