namespace ModulerUISystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class AbstractCameraStackManager : MonoBehaviour
    {
        public abstract void AddCameras(List<Camera> cameras);
        public abstract void RemoveCameras(List<Camera> cameras);
    }

}