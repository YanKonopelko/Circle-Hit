using System.Collections.Generic;
using NTC.Global.System;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public enum SoundType { DEATH = 0, VOLCANO, JUMP };

    private List<AudioSource> source = new List<AudioSource>();
    public int sourceAmount = 3;

    public AudioClip SecretSound;
    public AudioClip DeathSound;
    public AudioClip JumpSound;
    public AudioClip VolcanoSound;

    public  float volume = 0.4f;

    private KeyCode[] secret = new KeyCode[] { KeyCode.I, KeyCode.O, KeyCode.P };
    private int index = 0;

    private void Start()
    {
        volume = (PlayerPrefs.HasKey("VFX_VOLUME")) ? PlayerPrefs.GetFloat("VFX_VOLUME") : volume;

        for (int i = 0; i < sourceAmount; i++)
        {
            source.Add(gameObject.AddComponent<AudioSource>());
        }



        foreach (AudioSource aso in source)
        {
            aso.volume = volume;
        }
    }

    public  void ChangeVolume(float newVolume)
    {
        volume = newVolume;
        foreach (AudioSource aso in source)
        {
            aso.volume = volume;
        }
    }

    public  void PlaySound(SoundType type)
    {
        switch (type)
        {
            case (SoundType.DEATH):
                {
                    PlayLocal(DeathSound);
                    break;
                }
            case (SoundType.VOLCANO):
                {
                    PlayLocal(SecretSound);
                    break;
                }
            case (SoundType.JUMP):
                {
                    PlayLocal(JumpSound);
                    break;
                }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        PlayLocal(clip);
    }

    private void PlayLocal(AudioClip clip)
    {
        for (int i = 0; i < source.Count; i++)
        {
            if (source[i].isPlaying)
                continue;
            source[i].clip = clip;
            source[i].Play();
            return;
        }

        int rand = Random.Range(0, source.Count);
        source[rand].clip = clip;
        source[rand].Play();
    }

    private void Update()
    {
        if (Input.anyKeyDown) {

            if (Input.GetKeyDown(secret[index])) {
                index++;
            }
            else
            {
                if (index != 0)
                    index = 0;
            }

            if (index == secret.Length)
            {
                PlaySound(SecretSound);
                index = 0;
            }
        }
    }

}