using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModulerUISystem
{
    public class ResetSafeArea : MonoBehaviour
    {
        [SerializeField] private SafeAreaData safeAreaData;

        private void Awake()
        {
            safeAreaData.isCalibrated = false;
            Destroy(this);
        }
    }
}