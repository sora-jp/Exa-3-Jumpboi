using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Sirenix.OdinInspector;
using UnityBase.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : SerializedMonoBehaviour
{
    public static AudioManager Instance { get; set; }
    [Range(0, 1)] public float musicVolume;
    [Range(0, 1)] public float sfxVolume;
    public AudioSource musicSource, sfxSource;
    public AudioClip[] sceneMusic;
    public Dictionary<string, AudioClip> soundEffects;

    int m_curPlayingIdx = -1;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        SceneManager.activeSceneChanged += ChangeMusic;
        DontDestroyOnLoad(gameObject);
        if (musicSource == null || sfxSource == null) return;

        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    void ChangeMusic(Scene current, Scene next)
    {
        if (musicSource == null) return;
        musicSource.volume = musicVolume;
        if (m_curPlayingIdx == next.buildIndex) return;
        if (next.buildIndex >= sceneMusic.Length) return;
        m_curPlayingIdx = next.buildIndex;
        musicSource.clip = sceneMusic[next.buildIndex];
        musicSource.Play();
    }

    public static void PlayEffect(string effect) => Instance.Play(effect);

    public void Play(string effect)
    {
        if (soundEffects == null || sfxSource == null) return;
        if (!soundEffects.ContainsKey(effect))
        {
            Debug.LogWarning($"Attempted to play non-existent audio effect {effect}.");
            return;
        }

        var newSfx = Instantiate(sfxSource, transform);
        newSfx.clip = soundEffects[effect];
        newSfx.Play();

        Destroy(newSfx.gameObject, newSfx.clip.length + 0.1f);
    }

    public void Fade(float time)
    {
        if (musicSource == null) return;
        musicSource.AnimateVolume(0, time, EaseMode.Step4);
    }
}
