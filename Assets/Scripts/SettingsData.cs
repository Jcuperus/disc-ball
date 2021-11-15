using System;
using System.IO;
using UnityEngine;

[Serializable]
public class SettingsData
{
    [Serializable]
    public struct ResolutionData
    {
        public int height, width, refreshRate;

        public static ResolutionData FromResolution(Resolution resolution)
        {
            var resolutionData = new ResolutionData
            {
                height = resolution.height,
                width = resolution.width,
                refreshRate = resolution.refreshRate
            };

            return resolutionData;
        }

        public Resolution ToResolution()
        {
            var resolution = new Resolution
            {
                height = height,
                width = width,
                refreshRate = refreshRate
            };

            return resolution;
        }
    }
    
    public ResolutionData resolution;
    public bool isWindowed;
    public float sfxVolume, ambienceVolume;

    public const string FileName = "settings.json";
    
    private static string Path => string.Join("/", Application.persistentDataPath, FileName);

    public static void Save(SettingsData settingsData)
    {
        File.WriteAllText(Path, JsonUtility.ToJson(settingsData));
    }

    public static SettingsData Load()
    {
        string path = Path;
        SettingsData data;

        if (File.Exists(path))
        {
            data = JsonUtility.FromJson<SettingsData>(File.ReadAllText(path));
        }
        else
        {
            data = GetDefaultSettings();
            Save(data);
        }

        return data;
    }

    private static SettingsData GetDefaultSettings()
    {
        return new SettingsData
        {
            resolution = ResolutionData.FromResolution(Screen.currentResolution),
            isWindowed = false,
            sfxVolume = 1f,
            ambienceVolume = 1f
        };
    }
}