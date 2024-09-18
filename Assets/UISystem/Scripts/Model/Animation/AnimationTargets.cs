using System.Linq;
#if UNITY_EDITOR
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
#endif

namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    public class AnimationTargets : MonoBehaviour
    {
        public List<KeyValuePair<string, AnimationPropagator>> animationPropagators;
        private Dictionary<string, AnimationPropagator> propagators;

        private void OnEnable()
        {
            propagators = animationPropagators.ConvertToDictionary();
        }
        public void UpdateAnimations(string propogatorName,AnimatorOverrideController overrideController)
        {
            if(!propagators.ContainsKey(propogatorName))
                return;
            propagators[propogatorName].UpdateAnimations(overrideController);
        }

        public void TriggerAnimation(string parameterName)
        {
            foreach (var propagator in propagators.Values)
            {
                propagator.TriggerAnimation(parameterName);
            }
        }
        public void TriggerShowAnimation()
        {
            for (int indexOfElement = 0; indexOfElement <animationPropagators.Count ; indexOfElement++)
            {
                animationPropagators[indexOfElement].value.TriggerAnimation(UIConstants.PopInDefault);
            }
        }
        public void TriggerHideAnimation()
        {
            for (int indexOfElement = 0; indexOfElement <animationPropagators.Count ; indexOfElement++)
            {
                animationPropagators[indexOfElement].value.TriggerAnimation(UIConstants.PopOutDefault);
            }
        }
        public void TriggerShowWithoutDefault()
        {
            for (int indexOfElement = 0; indexOfElement <animationPropagators.Count ; indexOfElement++)
            {
                if (animationPropagators[indexOfElement].key == UIConstants.Default)
                    continue;
                
                animationPropagators[indexOfElement].value.TriggerAnimation(UIConstants.PopInDefault);
            }
        }
        public void TriggerHideWithoutDefault()
        {
            for (int indexOfElement = 0; indexOfElement <animationPropagators.Count ; indexOfElement++)
            {
                if (animationPropagators[indexOfElement].key == UIConstants.Default)
                    continue;
                
                animationPropagators[indexOfElement].value.TriggerAnimation(UIConstants.PopOutDefault);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("CacheAnimationTargets")]
        public void CacheAnimationTargets()
        {
            AnimationPropagator[] propagators = GetComponentsInChildren<AnimationPropagator>();
            animationPropagators = new List<KeyValuePair<string, AnimationPropagator>>();
            for (int indexOfAnimationPropagator = 0;
                indexOfAnimationPropagator < propagators.Length;
                indexOfAnimationPropagator++)
            {
                animationPropagators.Add(new KeyValuePair<string, AnimationPropagator>(
                    propagators[indexOfAnimationPropagator].name, propagators[indexOfAnimationPropagator]));
            }
            Debug.Log("Called");
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                EditorSceneManager.MarkSceneDirty(prefabStage.scene);
            }
        }

     
#endif

        public SlicePanel[] GetDefaultSlicePanels()
        {
            return propagators[UIConstants.Default].associatedPanels.ToArray();
        }
    }
}