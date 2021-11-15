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

    public void Play(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        this.DelayedAction(() => gameObject.SetActive(false), clip.length);
    }
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}