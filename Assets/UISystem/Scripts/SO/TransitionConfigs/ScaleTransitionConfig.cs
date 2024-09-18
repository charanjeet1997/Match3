
namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    [CreateAssetMenu(menuName = "UISystem/TransitionConfig/ScaleTransitionConfig")]
    public class ScaleTransitionConfig : TransitionConfig
    {
        #region PUBLIC_VARS
        public Vector3 targetScale;
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
            if (transitionType == TransitionType.Show)
            {
                return new ScaleShowTransition(duration,slicePanels,targetScale,animationCurve);
            }
            return new ScaleHideTransition(duration,targetScale,slicePanels,animationCurve);
        }
        #endregion

        #region PRIVATE_METHODS
        #endregion
    }
}