ios使用注意：
--使用麦克风权限
在info中添加Privacy - Microphone Usage Description描述
--使用相机权限
在info中添加Privacy - Camera Usage Description描述
--使用读取相册权限
在info中添加Privacy - Photo Library Additions Usage Description描述和Privacy - Photo Library Usage Description描述
还需要在TARGETS-UnityFramework-General-Frameworks and Libraries中添加Photos.framework

安卓使用注意：
需修改Unity 默认AndroidManifest文件，文件路径为
Mac系统：Unity.app安装同级目录： PlaybackEngines/AndroidPlayer/Apk/
Win系统：Unity安装目录下面（如果hub安装的就有hub，非hub安装的就少hub这一层，可以桌面右键图标打开文件所在位置）C:\Program Files\Unity\Hub\Editor\2019.4.5f1\Editor\Data\PlaybackEngines\AndroidPlayer\Apk
在文件中添加需要的权限：
--使用麦克风权限
    <uses-permission android:name="android.permission.RECORD_AUDIO" />
--使用相机权限
    <uses-permission android:name="android.permission.CAMERA" />
--使用读取相册权限
    在unity导出设置othersetting中的write permission(写入权限)设置为External（SDCard）