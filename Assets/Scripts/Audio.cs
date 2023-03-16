﻿using UnityEngine;

[System.Serializable]
public class Audio
{
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private AudioClip defaultClip;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;
    
    private AudioSource sourceSFX;
    private AudioSource sourceMusic;
    private AudioSource sourceRandomPitchSFX;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    public float MusicVolume
    {
        get
        {
            return musicVolume;
        }
        set
        {
            musicVolume = value;
            SourceMusic.volume = musicVolume;
        }
    }

    public float SfxVolume
    {
        get
        {
            return sfxVolume;
        }
        set
        {
            sfxVolume = value;
            SourceSFX.volume = sfxVolume;
            SourceRandomPitchSFX.volume = sfxVolume;
        }
    }

    public AudioSource SourceSFX
    {
        get => sourceSFX;
        set => sourceSFX = value;
    }
    
    public AudioSource SourceMusic
    {
        get => sourceMusic;
        set => sourceMusic = value;
    }
    
    public AudioSource SourceRandomPitchSFX
    {
        get => sourceRandomPitchSFX;
        set => sourceRandomPitchSFX = value;
    }
    
    private AudioClip GetSound(string clipName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == clipName)
            {
                return sounds[i];
            }
        }

        Debug.LogError("Can not find clip " + clipName);

        return defaultClip;
    }
    
    public void PlaySound(string clipName)
    {
        SourceSFX.PlayOneShot(GetSound(clipName), SfxVolume);
    }
    
    public void PlaySoundRandomPitch(string clipName)
    {
        SourceRandomPitchSFX.pitch = Random.Range(0.7f, 1.3f);
        SourceRandomPitchSFX.PlayOneShot(GetSound(clipName), SfxVolume);
    }
    
    public void PlayMusic(bool menu)
    {
        if (menu)
        {
            SourceMusic.clip = menuMusic;
        }
        else
        {
            SourceMusic.clip = gameMusic;
        }

        SourceMusic.volume = MusicVolume;
        SourceMusic.loop = true;
        SourceMusic.Play();
    }
}
