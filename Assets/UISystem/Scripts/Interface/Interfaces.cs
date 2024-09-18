namespace ModulerUISystem
{
    using System.Collections.Generic;
    using UnityEngine;
    public interface ICameraStackManager
    {
        void AddCameras(List<Camera> cameras);
        void RemoveCameras(List<Camera> cameras);
    }
    public interface ICanvasSortingOrderManager
    {
        void AddCanvases(List<Canvas> canvases);
        void RemoveCanvases(List<Canvas> canvases);
    }
    public interface INavigatorCallback
    {
        void ResetCallback();
        void OnNavigationStarted(Navigator navigator, TransitionType transitionType);
        void OnNavigationComplete(Navigator navigator, TransitionType transitionType);
        void OnNavigationRunning(Navigator navigator, TransitionType transitionType, float percentage);
    }
}
