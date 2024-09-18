namespace ModulerUISystem.Editor
{
    using UnityEngine;
    using UnityEditor;
    [InitializeOnLoad]
    public class MaintainCameraListInSlice
    {
        #region PUBLIC_VARS
        #endregion

        #region PRIVATE_VARS

        #endregion

        #region EDITOR_CALLBACKS
        static MaintainCameraListInSlice()
        {
            EditorApplication.update -= EditorUpdate;
            EditorApplication.update += EditorUpdate;
        }
        public static void EditorUpdate()
        {
            Slice[] slices = GameObject.FindObjectsOfType<Slice>();
            foreach (var slice in slices)
            {
                slice.cameraList.Clear();
                Transform cameraContainer = slice.transform.GetChild(0).transform;
                for (int indexOfChild = 0; indexOfChild < cameraContainer.childCount; indexOfChild++)
                {
                    Camera camera = cameraContainer.GetChild(indexOfChild).GetComponent<Camera>();
                    camera.depth = indexOfChild + 1;
                    slice.cameraList.Add(camera);
                }
            }
        }

        #endregion

        #region PUBLIC_METHODS

        #endregion

        #region PRIVATE_METHODS

        #endregion
    }
}
