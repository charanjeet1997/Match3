using System;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using UnityEngine;

public class EnhancedScrollerCellViewScaleController : MonoBehaviour
{
    [SerializeField] private EnhancedScroller _scroller;
    [SerializeField] private Transform referenceTransform;
    private bool isSampled = false;
    private float referenceDistance;

    // private void LateUpdate()
    // {
    //     if (_scroller.ActiveCellViews.Count < 2)
    //         return;
    //     // if (!isSampled)
    //     // {
    //     //     isSampled = true;
    //         // referenceDistance = Vector3.Distance(_scroller.ActiveCellViews[0].transform.position,
    //         //     _scroller.ActiveCellViews[1].transform.position);
    //         referenceDistance = Mathf.Abs(_scroller.ActiveCellViews[1].transform.position.y -
    //                                       _scroller.ActiveCellViews[0].transform.position.y);
    //     // }
    //
    //     for (int indexOfChild = 0; indexOfChild < _scroller.ActiveCellViews.Count; indexOfChild++)
    //     {
    //         float distance = Mathf.Abs(_scroller.ActiveCellViews[indexOfChild].transform.position.y - _scroller.ActiveCellViews[1].transform.position.y);
    //         _scroller.ActiveCellViews[indexOfChild].transform.localScale = Vector3.one * Mathf.Lerp(1f, .45f, Mathf.Clamp01(distance / referenceDistance));
    //         Debug.Log("EnhancedScrollerCellViewScaleController Runing : " + Mathf.Clamp01(distance / referenceDistance));
    //         Debug.Log("Distance : " + distance + " : REference Distance : " + referenceDistance);
    //     }
    // }
}