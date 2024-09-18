

namespace ModulerUISystem
{
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    using UnityEngine.UI;
    public abstract class TransitionConfig : ScriptableObject
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

        public virtual Transition GetTransition(TransitionType transitionType,SlicePanel[] slices,Slice[] slice)
        {
            return new Transition();
        }
        #endregion 
    }
    
}