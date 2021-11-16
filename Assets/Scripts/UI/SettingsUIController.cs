using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private Toggle windowedToggle;
        [SerializeField] private Slider sfxVolumeSlider, ambienceVolumeSlider;
        [SerializeField] private Button applyButton;

        private SettingsData settingsData;
        
        private void Awake()
        {
            settingsData = SettingsManager.SettingsData;
            
            PopulateResolutionOptions();
            windowedToggle.isOn = settingsData.isWindowed;
            sfxVolumeSlider.value = settingsData.sfxVolume;
            ambienceVolumeSlider.value = settingsData.ambienceVolume;
            
            applyButton.onClick.AddListener(OnApplyClicked);
        }

        private void PopulateResolutionOptions()
        {
            Resolution[] resolutions = Screen.resolutions;
            
            for (int i = 0; i < resolutions.Length; i++)
            {
                var resolutionOption = new TMP_Dropdown.OptionData(resolutions[i].ToString());
                resolutionDropdown.options.Add(resolutionOption);
                
                if (resolutions[i].Equals(settingsData.resolution.ToResolution())) resolutionDropdown.value = i;
            }
        }
        
        private void OnApplyClicked()
        {
            Resolution resolution = Screen.resolutions[resolutionDropdown.value];
            settingsData.resolution = SettingsData.ResolutionData.FromResolution(resolution);
            settingsData.isWindowed = windowedToggle.isOn;
            settingsData.sfxVolume = sfxVolumeSlider.value;
            settingsData.ambienceVolume = ambienceVolumeSlider.value;
            
            SettingsManager.ApplySettings(settingsData);
        }
    }
}


