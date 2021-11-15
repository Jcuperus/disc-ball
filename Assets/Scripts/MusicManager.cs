using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip ambienceClip;
    
    private AudioSource audioSource;
    private SettingsData settingsData;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        settingsData = SettingsManager.SettingsData;
    }

    private void OnEnable()
    {
        audioSource.volume = settingsData.ambienceVolume;
        audioSource.loop = true;
        audioSource.clip = ambienceClip;
        audioSource.Play();
    }

    private void OnDisable()
    {
        audioSource.Stop();
    }
}
