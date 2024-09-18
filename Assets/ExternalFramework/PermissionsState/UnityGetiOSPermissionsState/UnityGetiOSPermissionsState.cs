#if UNITY_IOS||UNITY_IPHONE  || UNITY_EDITOR
using System.Runtime.InteropServices;
using UnityEngine;
/// <summary>
/// iOS权限静态类
/// </summary>
public class UnityGetiOSPermissionsState : MonoBehaviour
{
    /// <summary>
    /// 首次调用获取麦克风权限
    /// </summary>
    [DllImport("__Internal")]
    public static extern void GetMicrophonePermission();

    /// <summary>
    /// 获取麦克风权限准许状态
    /// </summary>
    [DllImport("__Internal")]
    public static extern bool GetMicrophonePermissionState();

    /// <summary>
    /// 首次调用获取相机权限
    /// </summary>
    [DllImport("__Internal")]
    public static extern void GetCameraPermission();

    /// <summary>
    /// 获取相机权限准许状态
    /// </summary>
    [DllImport("__Internal")]
    public static extern bool GetCameraPermissionState();

    /// <summary>
    /// 首次调用获取相册权限
    /// </summary>
    [DllImport("__Internal")]
    public static extern void GetPhotoPermission();

    /// <summary>
    /// 获取相册权限准许状态
    /// </summary>
    [DllImport("__Internal")]
    public static extern bool GetPhotoPermissionState();

    /// <summary>
    /// 打开本程序的设置
    /// </summary>
    [DllImport("__Internal")]
    public static extern void OpenAppSettings();
}
#endif