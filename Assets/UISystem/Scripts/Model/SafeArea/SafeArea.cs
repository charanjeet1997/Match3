using System;
using EventManagement;
using Games.UnnamedArcade3d.Entities.LittleRed;

namespace ModulerUISystem
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;

    public class SafeArea : MonoBehaviour
    {
        [SerializeField] private RectTransform safeAreaRect;
        [SerializeField] private RectTransform viewContentRect;
        [SerializeField] private SafeAreaData _safeAreaData;
        [SerializeField] private OrientationSwitcher orientationSwitcher;

        private void OnEnable()
        {
            orientationSwitcher.OnTargetOrientationUpdated += ApplySafeArea;
        }

        private void OnDisable()
        {
            orientationSwitcher.OnTargetOrientationUpdated -= ApplySafeArea;            
        }

        private void Awake()
        {
            if (_safeAreaData.isCalibrated)
            {
                ApplySafeArea();
            }
        }

        IEnumerator Start()
        {
            yield return null;
            if (!_safeAreaData.isCalibrated)
            {
                ApplySafeArea(GetSafeArea());
            }
        }

        Rect GetSafeArea()
        {
            return Screen.safeArea;
        }

        void ApplySafeArea(Rect r)
        {
            // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
            Vector2 anchorMin = r.position;
            Vector2 anchorMax = r.position + r.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            
            _safeAreaData.anchoreMax = anchorMax;
            _safeAreaData.isCalibrated = true;
            
            
            
            
            // viewContentRect.anchorMax = anchorMax;
            // safeAreaRect.anchorMin = new Vector2(0, anchorMax.y);
            // safeAreaRect.anchorMax = Vector2.one;
            ApplySafeArea();
        }

        void ApplySafeArea()
        {
            if (orientationSwitcher.TargetOrientation == ScreenOrientation.Portrait)
            {
                safeAreaRect.anchorMin = new Vector2(0, _safeAreaData.anchoreMax.y);
                safeAreaRect.anchorMax = Vector2.one;
                
                viewContentRect.anchorMin = Vector2.zero;
                viewContentRect.anchorMax = new Vector2(1f,_safeAreaData.anchoreMax.y); 
                
            }
            else if(orientationSwitcher.TargetOrientation == ScreenOrientation.LandscapeLeft)
            {
                // Debug.Log("Safe Area B");
                viewContentRect.anchorMin = new Vector2(1f-_safeAreaData.anchoreMax.y, 0f);                
                viewContentRect.anchorMax = Vector2.one;
                
                safeAreaRect.anchorMin = Vector2.zero;
                safeAreaRect.anchorMax = new Vector2(1f-_safeAreaData.anchoreMax.y, 1f);   
            }
        }
    }
}