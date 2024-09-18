using UnityEngine;
using UnityEngine.Rendering;

public class QualitySettingsController : MonoBehaviour
{
    private int targetLevel;

    void Start()
    {
        targetLevel = GetPerformanceLevel();
        QualitySettings.SetQualityLevel(targetLevel, true);
        Destroy(this);
    }

    private int GetPerformanceLevel()
    {
        int level = QualitySettings.names.Length - 1;

        // Processor
        if (SystemInfo.processorCount <= 2)
            level = Mathf.Min(level, 2);

        // RAM
        if (SystemInfo.systemMemorySize <= 512)
            level = Mathf.Min(level, 1);

        // GPU
        if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2 || 
            SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3 || 
            SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLCore)
        {
            level = Mathf.Min(level, 1);
        }

        // Storage
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (SystemInfo.systemMemorySize <= 16)
                level = Mathf.Min(level, 0);
        }

        // Display Resolution
        if (Screen.width * Screen.height <= 1048576)
            level = Mathf.Min(level, 2);

        // Battery Life
        if (SystemInfo.batteryStatus == BatteryStatus.Charging || 
            SystemInfo.batteryStatus == BatteryStatus.Unknown || 
            SystemInfo.batteryLevel >= 0.75f)
        {
            level = Mathf.Min(level, 2);
        }
        else if (SystemInfo.batteryLevel >= 0.5f)
        {
            level = Mathf.Min(level, 1);
        }
        else
        {
            level = Mathf.Min(level, 0);
        }

        // Operating System
        if (Application.platform == RuntimePlatform.Android)
        {
            if (SystemInfo.operatingSystem.Contains("4.4"))
                level = Mathf.Min(level, 1);
        }
        Debug.Log("level: " + level );
        return level;
    }
}
