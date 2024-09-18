using System;

namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Navigation : MonoBehaviour
    {
        #region PUBLIC_VARS
        public ViewConfig viewConfig;
        [SerializeField] private bool showOnStart;
       
        #endregion

        #region PRIVATE_VARS

        #endregion

        #region UNITY_CALLBACKS

        private void OnEnable()
        {
            if (showOnStart)
            {
                ShowOverlay();
            }
        }

        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS

        public void ExecuteNavigation()
        {
            UIManager.instance.ShowView(viewConfig);
        }

        public void HideView()
        {
            UIManager.instance.HideView(viewConfig);
        }
        public void PrintSomthing(string path)
        {
            Debug.Log(path);
        }
        public void HideCurrentView()
        {
            UIManager.instance.HideCurrentView();
        }
        public void ShowOverlay()
        {
            UIManager.instance.ShowOverlay(viewConfig);
        }

        public void HideCurrentOverlay()
        {
            UIManager.instance.HideCurrentOverlay();
        }

        public void ShowLastView()
        {
            UIManager.instance.ShowLastView();
        }
        #endregion
    }

}