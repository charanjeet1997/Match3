using System.Collections;
using System.Collections.Generic;
using ModulerUISystem;
using UnityEngine;

public class TestOverlay : MonoBehaviour
{
    public ViewConfig overlay;
    public ViewConfig view;
    [ContextMenu("ShowOverlay")]
    public void ShowOverlay()
    {
        UIManager.instance.ShowOverlay(overlay);
    }
    
    [ContextMenu("HideOverlay")]
    public void HideOverlay()
    {
        UIManager.instance.HideOverlay(overlay);        
    }
    [ContextMenu("ShowView")]
    public void ShowView()
    {
        UIManager.instance.ShowView(view);
    }
}
