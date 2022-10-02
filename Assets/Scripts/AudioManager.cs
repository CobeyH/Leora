using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public Sound[] musicTracks;

    public AudioClip themeClip;

    private AudioSource mainTheme;

    public AudioMixerGroup mainMixer;

    public static AudioManager instance;

    private Sound currentSong;
    private List<AudioSource> activeAudioSources = new List<AudioSource>();

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        // Move the object into the root so it can be destroyed on load
        transform.parent = null;
        DontDestroyOnLoad(gameObject);

        AddAudioSources(sounds);
        AddAudioSources(musicTracks);
        mainTheme = gameObject.AddComponent<AudioSource>();
        mainTheme.clip = themeClip;
        mainTheme.volume = 0.5f;
    }

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (
            currentScene.name == "LevelSelector" ||
            currentScene.name == "StartScreen"
        )
        {
            mainTheme.Play();
        }
        else
        {
            PlayMusic();
        }
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
        // Prevent two songs from being played at once
        if (currentSong != null && currentSong.source.isPlaying == true)
        {
            return;
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

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            s.source.Stop();
            return;
        }
    }

    public void PauseAllSounds()
    {
        activeAudioSources.Clear();
        foreach (Sound s in sounds)
        {
            if (s.source.isPlaying)
            {
                activeAudioSources.Add(s.source);
                s.source.Pause();
            }
        }
    }

    public void ResumeSounds()
    {
        foreach (AudioSource s in activeAudioSources)
        {
            s.UnPause();
        }
    }
}
