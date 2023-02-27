using NTC.Global.System;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    private bool isPlaying;
    
    private AudioSource _audioSource;
    public float volume = 0.1f;

    void Awake()
    {
        isPlaying = PlayerPrefs.GetInt("isPlaying") == 0;
        _audioSource = GetComponent<AudioSource>();
        if (isPlaying)
            _audioSource.Play();
        else
            _audioSource.Stop();
        
        volume = (PlayerPrefs.HasKey("MUSIC_VOLUME")) ? PlayerPrefs.GetFloat("MUSIC_VOLUME") : volume;
        _audioSource.volume = volume;
    }
    
    public void ChangeVolume(float newVolume)
    {
        volume = newVolume;
        _audioSource.volume = volume;
    }
}
