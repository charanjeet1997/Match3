#if UNITY_ANDROID
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Content;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class AutoBuildNaming : IPostprocessBuildWithReport
{
    private int callbackOrder1;
    public int callbackOrder => callbackOrder1;
    private int buildVersion = 0;
    private string buildVersionProp = "CurrentBuildVersion";
    
    public void OnPostprocessBuild(BuildReport report)
    {
        if (EditorPrefs.HasKey(buildVersionProp))
        {
            buildVersion = EditorPrefs.GetInt(buildVersionProp);
            buildVersion++;
        }
        EditorPrefs.SetInt(buildVersionProp, buildVersion);
        string buildPath = report.summary.outputPath;
        string fileExtention = Path.GetExtension(buildPath);
        string directoryName = Path.GetDirectoryName(buildPath);
        string fileName = $"{PlayerSettings.productName}{DateTime.Now.ToString("yyyyMMdd")}V{buildVersion}";
        File.Move(buildPath,$"{directoryName}/{fileName}{fileExtention}");
        Debug.Log($"File Name {buildPath} , FileExtention {fileExtention} , Directoryname {directoryName}");
    }
}
#endif