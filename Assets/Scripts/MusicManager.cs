using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip ambienceClip;
    
    private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audioSource.clip = ambienceClip;
        audioSource.Play();
    }

    private void OnDisable()
    {
        audioSource.Stop();
    }
}
