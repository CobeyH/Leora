using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public Sound[] musicTracks;

    public AudioClip themeClip;

    private AudioSource mainTheme;

    public AudioMixerGroup mainMixer;

    public static AudioManager instance;

    private Sound currentSong;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy (gameObject);
            return;
        }

        DontDestroyOnLoad (gameObject);
        AddAudioSources (sounds);
        AddAudioSources (musicTracks);
        mainTheme = gameObject.AddComponent<AudioSource>();
        mainTheme.clip = themeClip;
    }

    void Start()
    {
        mainTheme.Play();
    }

    public void PlayMusic()
    {
        // Sound nextSong = musicTracks[UnityEngine.Random.Range(0, musicTracks.Length - 1)];
        // nextSong.source.Play();
        // Invoke(nameof(PlayMusic), nextSong.clip.length);
        if (mainTheme.isPlaying)
        {
            mainTheme.Stop();
        }
        currentSong =
            musicTracks[UnityEngine.Random.Range(0, musicTracks.Length - 1)];
        currentSong.source.Play();
        Invoke(nameof(PlayMusic), currentSong.clip.length);
    }

    public void PlayMainTheme()
    {
        if (currentSong.source.isPlaying)
        {
            currentSong.source.Stop();
        }
        mainTheme.Play();
    }

    void AddAudioSources(Sound[] clips)
    {
        foreach (Sound s in clips)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = mainMixer;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Play();
            return;
        }
        else
        {
            Debug.LogWarning("Sound not found with name: " + name);
        }
    }
}
