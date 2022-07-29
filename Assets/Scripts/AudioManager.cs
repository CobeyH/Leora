using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] musicTracks;
    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        AddAudioSources(sounds);
        AddAudioSources(musicTracks);
    }

    void Start()
    {
        PlayMusic();
    }

    void PlayMusic()
    {
        Sound nextSong = musicTracks[UnityEngine.Random.Range(0, musicTracks.Length - 1)];
        nextSong.source.Play();
        Invoke(nameof(PlayMusic), nextSong.clip.length);
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
