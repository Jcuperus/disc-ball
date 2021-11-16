public static class SettingsManager
{
    public static SettingsData SettingsData => settingsData ??= SettingsData.Load();

    private static SettingsData settingsData;
}