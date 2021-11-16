using UnityEngine;

public static class SettingsManager
{
    public static SettingsData SettingsData
    {
        get
        {
            if (settingsData == null)
            {
                settingsData = SettingsData.Load();
                ApplySettings(settingsData);
            }

            return settingsData;
        }
    }

    private static SettingsData settingsData;

    public static void ApplySettings(SettingsData appliedSettings)
    {
        SettingsData.Save(appliedSettings);
        
        Screen.SetResolution(settingsData.resolution.width, settingsData.resolution.height,
            !settingsData.isWindowed, settingsData.resolution.refreshRate);
    }
}