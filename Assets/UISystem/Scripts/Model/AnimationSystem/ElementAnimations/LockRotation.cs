using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModulerUISystem
{
    public class LockRotation : MonoBehaviour
    {
        [SerializeField] private RectTransform[] _rectTransforms;

        private void LateUpdate()
        {
            for (int indexOfRect = 0; indexOfRect < _rectTransforms.Length; indexOfRect++)
            {
                _rectTransforms[indexOfRect].rotation = Quaternion.identity;
            }
        }
    }
}