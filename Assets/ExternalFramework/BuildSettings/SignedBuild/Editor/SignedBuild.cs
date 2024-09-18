using System;
using System.Diagnostics;
using System.IO;

namespace SignedBuild
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [System.Serializable]
    public class BuildSetting
    {
        public string version;
        public int versionCode;
        public string keystorePath;
        public string keystorePassword;
        public string aliasName;
        public string aliasPassword;
        public bool splitBinray;
        public string adbPath;
        public string adb;

        public BuildSetting()
        {
            version = PlayerSettings.bundleVersion;
            versionCode = PlayerSettings.Android.bundleVersionCode;
            keystorePath = PlayerSettings.Android.keystoreName;
            keystorePassword = PlayerSettings.Android.keystorePass;
            aliasName = PlayerSettings.Android.keyaliasName;
            aliasPassword = PlayerSettings.Android.keyaliasPass;
            adb = EditorPrefs.GetString("AndroidSdkRoot") + "\\platform-tools\\adb.exe";
            adbPath = EditorPrefs.GetString("AndroidSdkRoot") + "\\platform-tools\\";
        }
    }

    public class SignedBuild : EditorWindow
    {
        private string buildSettingPath;
        public BuildSetting buildSetting;
        public DefaultAsset keystoreAsset;

        [MenuItem("Tools/Signed Build/Build Helper")]
        static void Init()
        {
            GetWindow<SignedBuild>("Signed Build");
        }

        private void OnEnable()
        {
            buildSetting = JsonUtility.FromJson<BuildSetting>(EditorPrefs.GetString("BuildSettings"));
        }

        private void Awake()
        {
            buildSettingPath = Path.Combine(Application.dataPath, "buildData.json");
            Debug.Log(buildSettingPath);
            Debug.Log(Application.dataPath);
            if (buildSetting == null)
            {
                buildSetting = new BuildSetting();
            }
        }

        private void OnGUI()
        {
            //Drawing keystore field
            keystoreAsset =
                (DefaultAsset) EditorGUILayout.ObjectField("Select Keystore", keystoreAsset, typeof(DefaultAsset));


            // Debug.Log(EditorPrefs.GetString("AndroidSdkRoot"));
            if (keystoreAsset != null)
            {
                //Drawing Editor
                string path = Path.GetDirectoryName(AssetDatabase.GetAssetPath(keystoreAsset));

                var serializedObject = new SerializedObject(this);
                var serializedProperty = serializedObject.FindProperty(("buildSetting"));
                EditorGUILayout.PropertyField(serializedProperty, true);
                ApplyCustomBuildSettings();
                buildSetting.keystorePath = AssetDatabase.GetAssetPath(keystoreAsset);
                serializedObject.ApplyModifiedProperties();


                if (GUILayout.Button("Save Build Setting to JSON"))
                {
                    var jsonData = JsonUtility.ToJson(buildSetting, true);
                    EditorPrefs.SetString("BuildSettings", jsonData);
                    AssetDatabase.Refresh();
                    Debug.Log("Signed Build");
                }
            }

            if (GUILayout.Button("ExecuteCommand"))
            {
                ExecuteCommand();
            }
        }

        void ApplyCustomBuildSettings()
        {
            PlayerSettings.bundleVersion = buildSetting.version;
            PlayerSettings.Android.bundleVersionCode = buildSetting.versionCode;
            PlayerSettings.iOS.buildNumber = buildSetting.versionCode.ToString();

            PlayerSettings.Android.keystoreName = buildSetting.keystorePath;
            PlayerSettings.Android.keystorePass = buildSetting.keystorePassword;

            PlayerSettings.Android.keyaliasName = buildSetting.aliasName;
            PlayerSettings.Android.keyaliasPass = buildSetting.aliasPassword;
        }

        void ExecuteCommand()
        {
            // EditorCoroutineUtility.StartCoroutine(CommandExecuter(), this);
        }

        IEnumerator CommandExecuter()
        {
            string cmd = " shell am startservice -n com.xxx.xxx/.xxx";
            using (Process proc = new Process())
            {
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe");
                Debug.Log("A");

                procStartInfo.RedirectStandardInput = true;
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.RedirectStandardError = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                proc.StartInfo = procStartInfo;

                proc.Start();

                Debug.Log("C");
                proc.StandardInput.WriteLine(buildSetting.adb + " devices");

                // proc.StandardInput.Flush();
                // proc.StandardInput.Close();

                
                Debug.Log(proc.StandardOutput.ReadLine());
                Debug.Log(proc.StandardOutput.ReadLine());
                Debug.Log(proc.StandardOutput.ReadLine());
                Debug.Log(proc.StandardOutput.ReadLine());
                Debug.Log(proc.StandardOutput.ReadLine());
                Debug.Log(proc.StandardOutput.ReadLine());
                Debug.Log(proc.StandardOutput.ReadLine());




                // Debug.Log(buildSetting.adb+" devices");
                // proc.StandardInput.WriteLine(buildSetting.adb+" shell ip addr show lo"); // this will not work on some newer os
                // string deviceip = proc.StandardOutput.ReadLine();
                // Debug.Log(deviceip);
                // Debug.Log("E");
                //
                // proc.StandardInput.WriteLine(buildSetting.adb+" tcpip 5555"); // this will turn on ADB to wifi on port 5555
                // proc.StandardInput.WriteLine(buildSetting.adb+" connect " + deviceip + " 5555"); // this allows you to at this point unplug your device
                // proc.StandardInput.WriteLine(buildSetting.adb+" -s " + deviceip + ":5555" +  cmd);  // this connects running the command on the specific ip address so when you disconnect usb it will continue creating movie
                // Debug.Log("F");
                //
                // while (!proc.HasExited)
                // {
                //     yield return null;
                // }
                Debug.Log("G");
                yield return null;
            }
        }
    }
}