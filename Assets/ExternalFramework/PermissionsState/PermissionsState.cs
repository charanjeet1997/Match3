using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移动端权限状态
/// </summary>
public static class PermissionsState
{
    /// <summary>
    /// 获取麦克风权限状态
    /// ios使用此权限需在info中添加Privacy - Microphone Usage Description描述
    /// andriod使用此权限需在AndroidManifest文件中添加        <uses-permission android:name="android.permission.RECORD_AUDIO" />
    /// </summary>
    /// <returns>权限状态</returns>
    public static PermissionState GetMicrophonePermissionState()
    {
        PermissionState permissionState = PermissionState.NotAllow;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        permissionState = PermissionState.UnnecessaryPlatform;
#elif UNITY_IOS || UNITY_IPHONE
        permissionState = UnityGetiOSPermissionsState.GetMicrophonePermissionState()?PermissionState.Allow:PermissionState.NotAllow;
#elif UNITY_ANDROID
        AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.RECORD_AUDIO");
        switch (result)
        {
            case AndroidRuntimePermissions.Permission.Denied://权限被拒绝且不再询问
                permissionState = PermissionState.NotAllow;
                break;
            case AndroidRuntimePermissions.Permission.Granted://允许
                permissionState = PermissionState.Allow;
                break;
            case AndroidRuntimePermissions.Permission.ShouldAsk://拒绝权限但不拒绝询问
                permissionState = PermissionState.WaitAsk;
                break;
        }
#endif
        return permissionState;
    }

    /// <summary>
    /// 获取相机权限状态
    /// ios使用此权限需在info中添加Privacy - Camera Usage Description描述
    /// andriod使用此权限需在AndroidManifest文件中添加        <uses-permission android:name="android.permission.CAMERA" />
    /// </summary>
    /// <returns>权限状态</returns>
    public static PermissionState GetCameraPermissionState()
    {
        PermissionState permissionState = PermissionState.NotAllow;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        permissionState = PermissionState.UnnecessaryPlatform;
#elif UNITY_IOS || UNITY_IPHONE
        permissionState = UnityGetiOSPermissionsState.GetCameraPermissionState()?PermissionState.Allow:PermissionState.NotAllow;
#elif UNITY_ANDROID
        AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.CAMERA");
        switch (result)
        {
            case AndroidRuntimePermissions.Permission.Denied://权限被拒绝且不再询问
                permissionState = PermissionState.NotAllow;
                break;
            case AndroidRuntimePermissions.Permission.Granted://允许
                permissionState = PermissionState.Allow;
                break;
            case AndroidRuntimePermissions.Permission.ShouldAsk://拒绝权限但不拒绝询问
                permissionState = PermissionState.WaitAsk;
                break;
        }
#endif
        return permissionState;
    }

    /// <summary>
    /// 获取相册写入权限状态
    /// ios使用此权限需在info中添加Privacy - Photo Library Additions Usage Description描述和Privacy - Photo Library Usage Description描述。还需要在TARGETS-UnityFramework-General-Frameworks and Libraries中添加Photos.framework
    /// andriod使用此权限需在Unity-buildsetting-othersetting中的write permission(写入权限)为External（SDCard）
    /// </summary>
    /// <returns>权限状态</returns>
    public static PermissionState GetWritePhotoPermissionState()
    {
        PermissionState permissionState = PermissionState.NotAllow;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        permissionState = PermissionState.UnnecessaryPlatform;
#elif UNITY_IOS || UNITY_IPHONE
        permissionState = UnityGetiOSPermissionsState.GetPhotoPermissionState()?PermissionState.Allow:PermissionState.NotAllow;
#elif UNITY_ANDROID
        AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.WRITE_EXTERNAL_STORAGE");
        switch (result)
        {
            case AndroidRuntimePermissions.Permission.Denied://权限被拒绝且不再询问
                permissionState = PermissionState.NotAllow;
                break;
            case AndroidRuntimePermissions.Permission.Granted://允许
                permissionState = PermissionState.Allow;
                break;
            case AndroidRuntimePermissions.Permission.ShouldAsk://拒绝权限但不拒绝询问
                permissionState = PermissionState.WaitAsk;
                break;
        }
#endif
        return permissionState;
    }

    /// <summary>
    /// 获取相册读取权限状态
    /// ios使用此权限需在info中添加Privacy - Photo Library Additions Usage Description描述和Privacy - Photo Library Usage Description描述。还需要在TARGETS-UnityFramework-General-Frameworks and Libraries中添加Photos.framework
    /// andriod使用此权限需在Unity-buildsetting-othersetting中的write permission(写入权限)为External（SDCard）
    /// </summary>
    /// <returns>权限状态</returns>
    public static PermissionState GetReadPhotoPermissionState()
    {
        PermissionState permissionState = PermissionState.NotAllow;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        permissionState = PermissionState.UnnecessaryPlatform;
#elif UNITY_IOS || UNITY_IPHONE
        permissionState = UnityGetiOSPermissionsState.GetPhotoPermissionState()?PermissionState.Allow:PermissionState.NotAllow;
#elif UNITY_ANDROID
        AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.READ_EXTERNAL_STORAGE");
        switch (result)
        {
            case AndroidRuntimePermissions.Permission.Denied://权限被拒绝且不再询问
                permissionState = PermissionState.NotAllow;
                break;
            case AndroidRuntimePermissions.Permission.Granted://允许
                permissionState = PermissionState.Allow;
                break;
            case AndroidRuntimePermissions.Permission.ShouldAsk://拒绝权限但不拒绝询问
                permissionState = PermissionState.WaitAsk;
                break;
        }
#endif
        return permissionState;
    }

    /// <summary>
    /// 打开App设置界面
    /// </summary>
    /// <returns>权限状态</returns>
    public static void OpenAppSettings()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN

#elif UNITY_IOS || UNITY_IPHONE
        UnityGetiOSPermissionsState.OpenAppSettings();
#elif UNITY_ANDROID
        AndroidRuntimePermissions.OpenSettings();
#endif
    }
}

public enum PermissionState
{
    //未允许
    NotAllow,
    //允许
    Allow,
    //等待询问
    WaitAsk,
    //不需要的平台
    UnnecessaryPlatform
}
