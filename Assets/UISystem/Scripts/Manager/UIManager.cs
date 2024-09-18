using System;
using System.Threading.Tasks;
using Games.UnnamedArcade3d.Entities.LittleRed;
using UnityEngine.UIElements;

namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class UIManager : MonoBehaviour
    {
        #region PUBLIC_VARS
        public static UIManager instance;
        public ViewConfig startView;
        public NavigationManager navigationManager;
        public InteractionBlockerOverlayCanvas interactionBlocker;
        #endregion

        #region PRIVATE_VARS
        private List<ViewConfig> _viewConfigs;
        private List<ViewConfig> _overlayConfigs;
        private SliceManager _sliceManager;
        private ViewConfig lastView;
        #endregion

        #region UNITY_CALLBACKS

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            Initialize();
        }

        #endregion

        #region PRIVATE_METHODS
        private void Initialize()
        {
            _viewConfigs = new List<ViewConfig>();
            _overlayConfigs = new List<ViewConfig>();
            _sliceManager = GetComponentInChildren<SliceManager>();
            ShowView(startView);
        }
        public void ToggleInteractionBlocker(bool isEnable)
        {
            if (interactionBlocker.gameObject.activeSelf != isEnable)
            {
                interactionBlocker.gameObject.SetActive(isEnable);
            }
        }
        #endregion

        #region PUBLIC_METHODS
        public void ShowView(ViewConfig viewConfig)
        {
            if (viewConfig.hidePrevious && _viewConfigs.Count > 0)
            {
                ViewConfig tempViewConfig = _viewConfigs.ReturnLast();
                List<SliceConfig> sliceConfigToRemove = tempViewConfig.sliceConfigList.FilterSliceConfig(_sliceManager.GetSliceConfigsWhichAreThereInList(viewConfig.sliceConfigList));
                List<Slice> tempSlice = _sliceManager.GetSlices(ref sliceConfigToRemove);
                navigationManager.AddNavigation(tempViewConfig.GetHideTransitionConfig(tempSlice), TransitionType.Hide, tempSlice,tempViewConfig.blockInteractionDuringTransition,() => _sliceManager.RemoveSlices(sliceConfigToRemove));
            }
            List<Slice> slices = _sliceManager.AddSlices(viewConfig.sliceConfigList);
            navigationManager.AddNavigation(viewConfig.GetShowTransitionConfig(slices), TransitionType.Show, slices,viewConfig.blockInteractionDuringTransition,() => _viewConfigs.Add(viewConfig));
        }
        public void HideView(ViewConfig viewConfig)
        {
            if (_viewConfigs.Count <= 0)
                return;
            
            Debug.Log("ViewConfig count : "+_viewConfigs.Count);
            ViewConfig tempViewConfig = _viewConfigs.ReturnItem(viewConfig);
            if (tempViewConfig != null)
            {
                List<Slice> tempSlices = _sliceManager.GetSlices(tempViewConfig.sliceConfigList);
                navigationManager.AddNavigation(tempViewConfig.GetHideTransitionConfig(tempSlices), TransitionType.Hide,tempSlices,tempViewConfig.blockInteractionDuringTransition,() => _sliceManager.RemoveSlices(tempViewConfig.sliceConfigList));
            }
        }

        public void HideCurrentView()
        {
            if (_viewConfigs.Count > 0)
            {
                ViewConfig tempViewConfig = _viewConfigs.ReturnLast();
                List<Slice> tempSlice = _sliceManager.GetSlices(tempViewConfig.sliceConfigList);
                navigationManager.AddNavigation(tempViewConfig.GetHideTransitionConfig(tempSlice), TransitionType.Hide, tempSlice,tempViewConfig.blockInteractionDuringTransition,() => _sliceManager.RemoveSlices(tempViewConfig.sliceConfigList));
            }
        }

        public void ShowOverlay(ViewConfig viewConfig)
        {
            List<Slice> slices = _sliceManager.AddOverlaySlices(viewConfig.sliceConfigList);
            navigationManager.AddNavigation(viewConfig.GetShowTransitionConfig(slices), TransitionType.Show, slices,viewConfig.blockInteractionDuringTransition,() => _overlayConfigs.Add(viewConfig));
        }
        public void HideOverlay(ViewConfig viewConfig)
        {
            ViewConfig tempViewConfig = _overlayConfigs.ReturnItem(viewConfig);
            if (tempViewConfig != null)
            {
                List<Slice> tempSlices = _sliceManager.GetOverlaySlices(tempViewConfig.sliceConfigList);
                navigationManager.AddNavigation(tempViewConfig.GetHideTransitionConfig(tempSlices), TransitionType.Hide,tempSlices,tempViewConfig.blockInteractionDuringTransition,() => _sliceManager.RemoveOverlaySlices(tempViewConfig.sliceConfigList));
            }
        }
        public void HideCurrentOverlay()
        {
            if (_overlayConfigs.Count > 0)
            {
                ViewConfig tempViewConfig = _overlayConfigs.ReturnLast();
                if (tempViewConfig != null)
                {
                    List<Slice> tempSlices = _sliceManager.GetOverlaySlices(tempViewConfig.sliceConfigList);
                    navigationManager.AddNavigation(tempViewConfig.GetHideTransitionConfig(tempSlices),
                        TransitionType.Hide, tempSlices, tempViewConfig.blockInteractionDuringTransition,
                        () => _sliceManager.RemoveOverlaySlices(tempViewConfig.sliceConfigList));
                }
            }
        }

        public void InitLastView(ViewConfig lastView)
        {
            this.lastView = lastView;
        }

        public void ShowLastView()
        {
            ShowView(lastView);
        }
        #endregion
    }
}