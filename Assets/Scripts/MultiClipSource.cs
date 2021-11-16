using UnityEngine;

public class MultiClipSource : MonoBehaviour
{
    [SerializeField, Range(-3f, 3f)] private float minPitchRange = 1f, maxPitchRange = 1f;
    [SerializeField] private AudioClip[] clips;

    public void Play()
    {
        float pitch = Random.Range(minPitchRange, maxPitchRange);
        int clipIndex = Random.Range(0, clips.Length);
        SoundEffectManager.Play(clips[clipIndex], pitch);
    }
}