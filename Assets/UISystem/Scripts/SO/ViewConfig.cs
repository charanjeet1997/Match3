
namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
using System;

    [CreateAssetMenu(menuName = "UISystem/ViewConfig")]
    public class ViewConfig : ScriptableObject
    {
        #region PUBLIC_VARS
        public bool hidePrevious;
        public bool blockInteractionDuringTransition;
        
        public TransitionConfig showTransitionConfig;
        public TransitionConfig hideTransitionConfig;
        
        public List<SliceConfig> sliceConfigList;
        
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS

        #endregion

        #region PRIVATE_METHODS
        [ContextMenu("CheckSliceName")]
        public void CheckSliceName()
        {
            for (int indexOfSliceConfig = 0; indexOfSliceConfig < sliceConfigList.Count; indexOfSliceConfig++)
            {
                Debug.Log("slice Name : "+sliceConfigList[indexOfSliceConfig].sliceName);
            }
        }
        #endregion

        #region PUBLIC_METHODS
        // public TransitionConfig[] GetShowTransitionConfig()
        // {
        //     List<TransitionConfig> transitionConfigs = new List<TransitionConfig>();
        //     for (int indexOfSliceConfig = 0; indexOfSliceConfig < sliceConfigList.Count; indexOfSliceConfig++)
        //     {
        //         SliceConfig sliceConfig =  sliceConfigList[indexOfSliceConfig];
        //         if (sliceConfig.overrideViewShowTransition)
        //         {
        //             transitionConfigs.Add(sliceConfig.showTransitionConfig); 
        //         }
        //         else
        //         {
        //             transitionConfigs.Add(showTransitionConfig);
        //         }
        //     }
        //     return transitionConfigs.ToArray();
        // }
        //
        // public TransitionConfig[] GetHideTransitionConfig()
        // {
        //     List<TransitionConfig> transitionConfigs = new List<TransitionConfig>();
        //     for (int indexOfSliceConfig = 0; indexOfSliceConfig < sliceConfigList.Count; indexOfSliceConfig++)
        //     {
        //         SliceConfig sliceConfig =  sliceConfigList[indexOfSliceConfig];
        //         if (sliceConfig.overrideViewHideTransition)
        //         {
        //             transitionConfigs.Add(sliceConfig.hideTransitionConfig); 
        //         }
        //         else
        //         {
        //             transitionConfigs.Add(hideTransitionConfig);
        //         }
        //         
        //     }
        //     return transitionConfigs.ToArray();
        // }

        public TransitionConfig[] GetShowTransitionConfig(List<Slice> slices)
        {
            List<TransitionConfig> transitionConfigs = new List<TransitionConfig>();
            for (int indexOfSlice = 0; indexOfSlice < slices.Count; indexOfSlice++)
            {
                SliceConfig sliceConfig = sliceConfigList.Find(x => slices[indexOfSlice].name.Contains(x.sliceName));
                if (sliceConfig.overrideViewShowTransition)
                {
                    transitionConfigs.Add(sliceConfig.showTransitionConfig); 
                }
                else
                {
                    transitionConfigs.Add(showTransitionConfig);
                }
            }
            return transitionConfigs.ToArray();
        }
        public TransitionConfig[] GetHideTransitionConfig(List<Slice> slices)
        {
            List<TransitionConfig> transitionConfigs = new List<TransitionConfig>();
            for (int indexOfSlice = 0; indexOfSlice < slices.Count; indexOfSlice++)
            {
                SliceConfig sliceConfig = sliceConfigList.Find(x => slices[indexOfSlice].name.Contains(x.sliceName));
              
                if (sliceConfig.overrideViewHideTransition)
                {
                    transitionConfigs.Add(sliceConfig.hideTransitionConfig); 
                }
                else
                {
                    transitionConfigs.Add(hideTransitionConfig);
                }
            }
            return transitionConfigs.ToArray();
        }
        #endregion

        #region SLICE_CONFIG
        
        #endregion
    }
}
