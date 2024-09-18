
namespace ModulerUISystem
{
    using UnityEngine;
    using System;
    [CreateAssetMenu(menuName = "UISystem/TransitionConfig/InstantTransitionConfig")]
    public class InstantTransitionConfig : TransitionConfig
    {
        #region PUBLIC_VARS
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS
        #endregion

        #region PRIVATE_METHODS
        #endregion

        #region PUBLIC_METHODS
        public override Transition GetTransition(TransitionType transitionType,SlicePanel[] slicePanels,Slice[] slices)
        {
            return base.GetTransition(transitionType,slicePanels,slices);
        }
        #endregion
    }
}
