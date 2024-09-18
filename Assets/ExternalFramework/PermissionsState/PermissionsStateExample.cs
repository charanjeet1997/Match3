using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermissionsStateExample : MonoBehaviour
{
    public Button m_AskMicrophonePermissionStateButton;
    public Button m_AskCameraPermissionStateButton;
    public Button m_AskReadPhotoPermissionStateButton;
    public Button m_AskWritePhotoePermissionStateButton;
    public Button m_OpenAppSettingsButton;
    void Start()
    {
        m_AskMicrophonePermissionStateButton.onClick.AddListener(GetMicrophonePermissionState);
        m_AskCameraPermissionStateButton.onClick.AddListener(GetCameraPermissionState);
        m_AskReadPhotoPermissionStateButton.onClick.AddListener(GetReadPhotoPermissionState);
        m_AskWritePhotoePermissionStateButton.onClick.AddListener(GetWritePhotoPermissionState);
        m_OpenAppSettingsButton.onClick.AddListener(OpenAppSettings);
    }

    void Update()
    {

    }

    void GetMicrophonePermissionState()
    {
        PermissionState permissionState = PermissionsState.GetMicrophonePermissionState();

        switch (permissionState)
        {
            case PermissionState.Allow:
                Debug.Log("Microphone 权限允许");
                break;
            case PermissionState.NotAllow:
                Debug.Log("Microphone 权限不允许");
                break;
            case PermissionState.WaitAsk:
                Debug.Log("Microphone 等待询问");
                break;
            default: break;
        }
    }

    void GetCameraPermissionState()
    {
        PermissionState permissionState = PermissionsState.GetCameraPermissionState();

        switch (permissionState)
        {
            case PermissionState.Allow:
                Debug.Log("Camera 权限允许");
                break;
            case PermissionState.NotAllow:
                Debug.Log("Camera 权限不允许");
                break;
            case PermissionState.WaitAsk:
                Debug.Log("Camera 等待询问");
                break;
            default: break;
        }
    }

    void GetWritePhotoPermissionState()
    {
        PermissionState permissionState = PermissionsState.GetWritePhotoPermissionState();

        switch (permissionState)
        {
            case PermissionState.Allow:
                Debug.Log("WritePhoto 权限允许");
                break;
            case PermissionState.NotAllow:
                Debug.Log("WritePhoto 权限不允许");
                break;
            case PermissionState.WaitAsk:
                Debug.Log("WritePhoto 等待询问");
                break;
            default: break;
        }
    }

    void GetReadPhotoPermissionState()
    {
        PermissionState permissionState = PermissionsState.GetReadPhotoPermissionState();

        switch (permissionState)
        {
            case PermissionState.Allow:
                Debug.Log("ReadPhoto 权限允许");
                break;
            case PermissionState.NotAllow:
                Debug.Log("ReadPhoto 权限不允许");
                break;
            case PermissionState.WaitAsk:
                Debug.Log("ReadPhoto 等待询问");
                break;
            default: break;
        }
    }

    void OpenAppSettings()
    {
        PermissionsState.OpenAppSettings();
    }
}
