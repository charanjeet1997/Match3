namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class InteractionBlockerOverlayCanvas : MonoBehaviour
    {
        #region PUBLIC_VARS

        public GameObject interactionBlockerOverlayCanvas;

        #endregion

        #region PRIVATE_VARS

        #endregion

        #region UNITY_CALLBACKS

        public void ToggleCanvas(bool isEnable)
        {
            interactionBlockerOverlayCanvas.SetActive(isEnable);
        }

        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS

        #endregion
    }
}