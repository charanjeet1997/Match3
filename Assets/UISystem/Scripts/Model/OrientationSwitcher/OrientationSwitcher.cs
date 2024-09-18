using System;
using UnityEngine;

namespace Games.UnnamedArcade3d.Entities.LittleRed
{
    [CreateAssetMenu(menuName = "Data/OrientationSwitcher", fileName = "OrientationSwitcher")]
    public class OrientationSwitcher : ScriptableObject
    {
        public Action OnTargetOrientationUpdated = delegate { };
        private ScreenOrientation targetOrientation = ScreenOrientation.Portrait;

        private void OnEnable()
        {
            targetOrientation = ScreenOrientation.Portrait;
            // Debug.Log("Orientation data : ");
        }

        #region PUBLIC_VARS


        public ScreenOrientation TargetOrientation
        {
            get => targetOrientation;
            set
            {
                if (targetOrientation != value)
                {
                    // Debug.Log("Orientation Updated");
                    targetOrientation = value;
                    Screen.orientation = value;
                    OnTargetOrientationUpdated();
                }
            }
        }

        #endregion
    }
}