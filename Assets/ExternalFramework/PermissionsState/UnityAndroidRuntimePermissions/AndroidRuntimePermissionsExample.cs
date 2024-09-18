#if UNITY_ANDROID
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using UnityEngine.Android; 

public class AndroidRuntimePermissionsExample : MonoBehaviour
{
    //常用权限
    //android.permission.CAMERA 相机权限
    //android.permission.RECORD_AUDIO 麦克风权限
    //android.permission.READ_EXTERNAL_STORAGE 读储存卡，直接在设置中勾选write permission为External（SDCard）
    //android.permission.WRITE_EXTERNAL_STORAGE 写储存卡，直接在设置中勾选write permission为External（SDCard）

    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Clicked();
        }
    }

    public void Clicked()
    {
        AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission("android.permission.RECORD_AUDIO");
        switch (result)
        {
            case AndroidRuntimePermissions.Permission.Denied://永久拒绝询问
                Debug.Log("权限被拒绝且不再询问");
                AndroidRuntimePermissions.OpenSettings();// 打开本程序的设置界面
                break;
            case AndroidRuntimePermissions.Permission.Granted://允许
                Debug.Log("权限已开启");
                break;
            case AndroidRuntimePermissions.Permission.ShouldAsk://拒绝权限但不拒绝询问
                Debug.Log("权限被拒绝");
                break;
        }
    }
}
#endif