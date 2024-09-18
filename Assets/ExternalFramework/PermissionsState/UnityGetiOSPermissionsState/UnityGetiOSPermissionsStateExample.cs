#if UNITY_IOS||UNITY_IPHONE  || UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityGetiOSPermissionsStateExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void GetMicrophonePermission()
    {
        UnityGetiOSPermissionsState.GetMicrophonePermission();
    }
    public void GetMicrophonePermissionState()
    {
        bool state = UnityGetiOSPermissionsState.GetMicrophonePermissionState();
        Debug.Log("GetMicrophonePermissionState" + state);
    }
    public void GetCameraPermission()
    {
        UnityGetiOSPermissionsState.GetCameraPermission();
    }
    public void GetCameraPermissionState()
    {
        UnityGetiOSPermissionsState.GetCameraPermissionState();
    }
    public void GetPhotoPermission()
    {
        UnityGetiOSPermissionsState.GetPhotoPermission();
    }
    public void GetPhotoPermissionState()
    {
        UnityGetiOSPermissionsState.GetPhotoPermissionState();
    }
    public void OpenAppSettings()
    {
        UnityGetiOSPermissionsState.OpenAppSettings();
    }
}
#endif
