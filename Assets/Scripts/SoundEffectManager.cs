using UnityEngine;

public static class SoundEffectManager
{
    private static readonly ObjectPool<RecyclableAudioSource> AudioSourcePool = new ObjectPool<RecyclableAudioSource>();
    
    public static void Play(AudioClip clip, float pitch = 1f)
    {
        RecyclableAudioSource source = AudioSourcePool.GetObject();
        source.Pitch = pitch;
        source.gameObject.SetActive(true);
        source.Play(clip);
    }
}