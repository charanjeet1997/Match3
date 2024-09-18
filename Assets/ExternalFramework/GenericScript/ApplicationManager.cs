using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    #region PUBLIC_VARS

    [Range(0, 120)] public int targetFramerate;
    [Range(0, 4)] public int vSyncCount;

    public bool logEnabled;
    public bool runInBackground;

    #endregion

    #region PRIVATE_VARS

    #endregion

    #region UNITY_CALLBACKS

    private IEnumerator Start()
    {
        yield return null;
        Application.targetFrameRate = targetFramerate;
        Application.runInBackground = runInBackground;
        QualitySettings.vSyncCount = vSyncCount;
        Debug.unityLogger.logEnabled = logEnabled;
        // Destroy(this);
    }
    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     Application.Quit();
        // }
    }

    #endregion

    #region PUBLIC_METHODS

    #endregion

    #region PRIVATE_METHODS

    #endregion
}