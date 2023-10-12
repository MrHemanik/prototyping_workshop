using System;
using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioSource aus;
    void Awake()
    {
        //Only 1 AudioSource to abruptly change stuff
        aus = gameObject.AddComponent<AudioSource>();
    }

    public void Play(string soundName)
    {
        
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        aus.clip = s.clip;
        aus.volume = s.volume;
        aus.pitch = s.pitch;
        aus.Play();
    }

    public float GetSoundLength(string soundName)
    {
        return Array.Find(sounds, sound => sound.name == soundName).clip.length;
    }
}
