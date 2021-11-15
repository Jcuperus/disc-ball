using Helpers;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RecyclableAudioSource : MonoBehaviour
{
    public float Pitch
    {
        set => audioSource.pitch = value;
    }
    
    private AudioSource audioSource;
    private SettingsData settingsData;

    public void Play(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        audioSource.volume = settingsData.sfxVolume;
        this.DelayedAction(() => gameObject.SetActive(false), clip.length);
    }
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        settingsData = SettingsManager.SettingsData;
    }
}