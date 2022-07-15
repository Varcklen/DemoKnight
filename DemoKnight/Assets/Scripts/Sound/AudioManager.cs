using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {

        //if (instance == null)
            //instance = this;
        //else
        //{
            //Destroy(gameObject);
            //return;
        //}

        //DontDestroyOnLoad(gameObject);
        foreach( Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.isLoop;
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    public Sound Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("WARNING! Sound: " + name + " not found!");
            return s;
        }
        bool l = false;
        if (s.isMusic && Sound.isMusicActive)
        {
            s.source.volume = s.volume * Sound.musicVolume;
            Sound.music = s;
            l = true;
        }
        else if (Sound.isSoundActive)
        {
            s.source.volume = s.volume * Sound.soundVolume;
            l = true;
        }
        if (l)
            s.source.Play();
        return s;
    }
}
