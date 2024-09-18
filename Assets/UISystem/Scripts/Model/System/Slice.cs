
using DataBinding.Core;
using UnityEngine.SocialPlatforms;

namespace ModulerUISystem
{
    using UnityEngine;   
    using System;
    using System.Collections.Generic;
    using UnityEngine.Rendering.Universal;
    using System.Linq;
    using UnityEngine.UI;
    public class Slice : MonoBehaviour
    {
        #region PUBLIC_VARS
        public string sliceName;
        public CacheType cacheType;
        public List<Camera> cameraList;
        [HideInInspector]
        public List<Canvas> canvasList;
        public SlicePanel[] slicePanels;
        public AnimationTargets animationTargets;
        #endregion

        #region PRIVATE_VARS
       
        #endregion

        #region UNITY_CALLBACKS
        
        #endregion

        #region PRIVATE_METHODS
        #endregion

        #region PUBLIC_METHODS
        public void PrintSomething(string printSomething)
        {
            Debug.Log(printSomething);
        }
        #endregion
    }
}
