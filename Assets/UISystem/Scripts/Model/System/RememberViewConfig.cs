using System;

namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RememberViewConfig : MonoBehaviour
    {
        #region PUBLIC_VARS
        public ViewConfig viewToGoBackTo;
        #endregion

        #region PRIVATE_VARS

        #endregion

        #region UNITY_CALLBACKS

        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS

        public void AssignLastView()
        {
            UIManager.instance.InitLastView(viewToGoBackTo);
        }

        #endregion
    }
}