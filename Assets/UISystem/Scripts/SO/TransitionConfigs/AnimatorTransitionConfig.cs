
namespace ModulerUISystem
{
    using UnityEngine;
    using System;
    [CreateAssetMenu(menuName = "UISystem/TransitionConfig/AnimatorTransitionConfig")]
    public class AnimatorTransitionConfig : TransitionConfig
    {
        #region PUBLIC_VARS
        [SerializeField]
        private AnimatorOverrideController overrideController;
        [SerializeField]
        private string showTriggerName;
        [SerializeField]
        private string hideTriggerName;
        
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS

        #endregion

        #region PRIVATE_METHODS

        private float FindClipLength(string clipName)
        {
            for (int indexOfClip = 0; indexOfClip < overrideController.animationClips.Length; indexOfClip++)
            {
                if (overrideController.animationClips[indexOfClip].name == clipName)
                {
                    return overrideController.animationClips[indexOfClip].length;
                    
                }
            }
            return 0;
        }
        #endregion

        #region PUBLIC_METHODS
        public override Transition GetTransition(TransitionType transitionType,SlicePanel[] slicePanels,Slice[] slices)
        {
            if (transitionType == TransitionType.Show)
            {
                return new AnimatorTransitionData(overrideController,showTriggerName,slices,FindClipLength(showTriggerName),slicePanels);
            }
            return new AnimatorTransitionData(overrideController,hideTriggerName,slices,FindClipLength(hideTriggerName),slicePanels);
        }
        #endregion
    }
}
