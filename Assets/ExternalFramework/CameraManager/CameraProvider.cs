using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.CameraManager
{
    public class CameraProvider : MonoBehaviour
    {
        [SerializeField]
        private Camera camera;
        
        private void OnEnable()
        {
            ServiceLocatorFramework.ServiceLocator.Current.Get<ICameraManager>().UpdateCamera(camera);
            Destroy(this);
        }
    }
}