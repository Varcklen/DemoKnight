using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    private AudioManager scriptAudioManager;
    private bool isActive = false;
    public float soundDelay = 0.5f;
    private Sound soundSlash;

    void Start()
    {
        scriptAudioManager = FindObjectOfType<AudioManager>();
    }

    public void ChangeSound(float newFloat)
    {
        //Debug.Log("Time is: " + Time.deltaTime);
        //Debug.Log("lastTime is: " + lastSoundPlayed);
        Sound.soundVolume = newFloat;
        if (!isActive)
        {
            soundSlash = scriptAudioManager.Play("Sword Slash");
            isActive = true;
            StartCoroutine(EndCool());
        }
    }

    IEnumerator EndCool()
    {
        yield return new WaitForSeconds(soundDelay);
        isActive = false;
        soundSlash = null;
    }

    public void ChangeMusic(float newFloat)
    {
        Sound.musicVolume = newFloat;
        if (Sound.isMusicActive)
        {
            Sound.music.source.volume = Sound.music.volume * Sound.musicVolume;
        }
    }

    private void FixedUpdate()
    {
        if (soundSlash != null)
        {
            soundSlash.source.volume = soundSlash.volume * Sound.soundVolume;
            //Debug.Log("Changed");
        }
    }

    public void SwitchMusic(bool isMusicActive)
    {
        Sound.isMusicActive = isMusicActive;
        if (isMusicActive)
        {
            Sound.music.source.volume = Sound.music.volume * Sound.musicVolume;
        } else
        {
            Sound.music.source.volume = 0f;
        }
    }

    public void SwitchSound(bool isSoundActive)
    {
        Sound.isSoundActive = isSoundActive;
    }
}
