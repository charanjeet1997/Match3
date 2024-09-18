//namespace SignedBuild
//{
//#if UNITY_EDITOR && UNITY_ANDROID
//    using System.Collections;
//    using System.Collections.Generic;
//    using System.IO;
//    using UnityEditor;
//    using UnityEditor.Build.Content;
//    using UnityEngine;

//    public class BuildSignedAPK
//    {
//        private static BuildSetting buildSetting;
        

//        public static void PreExport()
//        {
//            Debug.Log("BuildAddressablesProcessor.PreExport start : "+buildSetting.aliasName);
//            Debug.Log(PlayerSettings.bundleVersion);
//            PlayerSettings.bundleVersion = buildSetting.version;
//            PlayerSettings.Android.bundleVersionCode = buildSetting.versionCode;
//            PlayerSettings.iOS.buildNumber = buildSetting.versionCode.ToString();
//            PlayerSettings.Android.keystoreName = buildSetting.keystorePath;
//            PlayerSettings.Android.keystorePass = buildSetting.keystorePassword;
//            PlayerSettings.Android.keyaliasName = buildSetting.aliasName;
//            PlayerSettings.Android.keyaliasPass = buildSetting.aliasPassword;
//            Debug.Log("BuildAddressablesProcessor.PreExport done");
//        }

//        [InitializeOnLoadMethod]
//        private static void Initialize()
//        {
//            // Debug.Log("Build Initialize");
//            BuildPlayerWindow.RegisterBuildPlayerHandler(BuildPlayerHandler);
//        }

//        private static void BuildPlayerHandler(BuildPlayerOptions options)
//        {
//            if (EditorUtility.DisplayDialog("Build Signed APK",
//                "Do you want to build a signed apk?",
//                "Build Signed APK", "Skip"))
//            { 
//                buildSetting = JsonUtility.FromJson<BuildSetting>(EditorPrefs.GetString("BuildSettings"));
//                PreExport();
//            }

//            BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);
//        }
//    }
//#endif
//}