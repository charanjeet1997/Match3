namespace ModulerUISystem.Editor
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEditor;
    [InitializeOnLoad]
    public class MaintainCanvasListInSlice
    {
        #region PUBLIC_VARS
        #endregion

        #region PRIVATE_VARS

        #endregion

        #region EDITOR_CALLBACKS
        static MaintainCanvasListInSlice()
        {
            EditorApplication.update -= EditorUpdate;
            EditorApplication.update += EditorUpdate;
        }
        public static void EditorUpdate()
        {
            Slice[] slices = GameObject.FindObjectsOfType<Slice>();
            foreach (var slice in slices)
            {
                slice.canvasList.Clear();
                Transform canvasContainer = slice.transform.GetChild(1).transform;
                for (int indexOfChild = 0; indexOfChild < canvasContainer.childCount; indexOfChild++)
                {
                    Canvas canvas = canvasContainer.GetChild(indexOfChild).GetComponent<Canvas>();
                    slice.canvasList.Add(canvas);
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
