using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public static float soundVolume = 0.7f;
    public static float musicVolume = 0.4f;
    public static bool isMusicActive = true;
    public static bool isSoundActive = true;

    public string name;
    public AudioClip clip;
    public bool isMusic;
    public bool isLoop;

    [Range(0f,1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
    [HideInInspector]
    public static Sound music;

}
