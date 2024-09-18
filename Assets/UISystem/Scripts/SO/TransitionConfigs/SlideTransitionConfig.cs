
namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    [CreateAssetMenu(menuName = "UISystem/TransitionConfig/SlideTransitionConfig")]
    public class SlideTransitionConfig : TransitionConfig
    {
        #region PUBLIC_VARS
        public Vector2 direction;
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
                return new SlideShowTransition(duration,slicePanels,direction,animationCurve);
            }
            return new SlideHideTransition(duration,direction,slicePanels,animationCurve);
        }
        #endregion

        #region PRIVATE_METHODS
        #endregion
    }
}