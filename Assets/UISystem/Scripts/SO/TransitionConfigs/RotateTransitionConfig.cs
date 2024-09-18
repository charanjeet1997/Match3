
namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    [CreateAssetMenu(menuName = "UISystem/TransitionConfig/RotateTransitionConfig")]
    public class RotateTransitionConfig : TransitionConfig
    {
        #region PUBLIC_VARS
        public float targetRotation;        
        public float duration;
        public AnimationCurve animationCurve;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS
        #endregion

        #region PUBLIC_METHODS

        public override Transition GetTransition(TransitionType transitionType,SlicePanel[] slicePanels,Slice[] slices)
        {
            if(transitionType==TransitionType.Show)
                return new RotationTransition(duration,slicePanels,targetRotation,animationCurve);
            return new RotationTransition(duration,slicePanels,targetRotation,animationCurve);
        }
        #endregion

        #region PRIVATE_METHODS
        #endregion
    }
}