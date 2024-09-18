namespace ModulerUISystem
{
    using UnityEngine;
    using System;
    using System.Collections.Generic;
    using UnityEngine.Rendering.Universal;

    public class URPCameraStackManager : AbstractCameraStackManager
    {
        #region PUBLIC_VARS

        private UniversalAdditionalCameraData _universalAdditionalCameraData;

        #endregion

        #region PRIVATE_VARS

        [SerializeField] private List<Camera> uiCameras;

        #endregion

        #region UNITY_CALLBACKS

        public void Start()
        {
            uiCameras = new List<Camera>();
            _universalAdditionalCameraData = Camera.main.GetUniversalAdditionalCameraData();
            // Debug.Log("URP Start called");
            
            if (_universalAdditionalCameraData == null)
            {
                Debug.Log("_Additional CameraData not found");
            }

            if (_universalAdditionalCameraData.cameraStack == null)
            {
                Debug.Log("Camera stack not found");
            }
        }

        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region PUBLIC_METHODS

        public override void AddCameras(List<Camera> cameraList)
        {
            foreach (var camera in cameraList)
            {
                if (!_universalAdditionalCameraData.cameraStack.Contains(camera))
                {
                    _universalAdditionalCameraData.cameraStack.Add(camera);

                    uiCameras.Add(camera);
                }
                else
                {
                    _universalAdditionalCameraData.cameraStack.Remove(camera);
                    _universalAdditionalCameraData.cameraStack.Add(camera);

                    uiCameras.Remove(camera);
                    uiCameras.Add(camera);
                }
            }
        }

        public override void RemoveCameras(List<Camera> cameraList)
        {
            foreach (var camera in cameraList)
            {
                if (_universalAdditionalCameraData.cameraStack.Contains(camera))
                {
                    _universalAdditionalCameraData.cameraStack.Remove(camera);
                    uiCameras.Remove(camera);
                }
            }
        }

        public void OnCameraUpdate(Camera camera)
        {
            // _universalAdditionalCameraData = camera.GetUniversalAdditionalCameraData();
            // if (_universalAdditionalCameraData.cameraStack == null)
            // {
            //     Debug.Log("Camera stack found null");
            // }
            // _universalAdditionalCameraData.cameraStack.Clear();
            // _universalAdditionalCameraData.cameraStack.AddRange(uiCameras);
        }

        #endregion
    }
}