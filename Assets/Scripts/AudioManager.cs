using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : SerializedMonoBehaviour
{
    public static AudioManager Instance { get; set; }
    public AudioClip[] sceneMusic;
    public Dictionary<string, AudioClip> soundEffects;

    AudioSource m_source;
    int m_curPlayingIdx = -1;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        m_source = GetComponent<AudioSource>();
        SceneManager.activeSceneChanged += ChangeMusic;
        DontDestroyOnLoad(gameObject);
    }

    void ChangeMusic(Scene current, Scene next)
    {
        if (m_curPlayingIdx == next.buildIndex) return;
        if (next.buildIndex >= sceneMusic.Length) return;
        m_curPlayingIdx = next.buildIndex;
        m_source.clip = sceneMusic[next.buildIndex];
        m_source.Play();
    }

    public static void PlayEffect(string effect) => Instance.Play(effect);

    public void Play(string effect)
    {
        if (!soundEffects.ContainsKey(effect))
        {
            Debug.LogWarning($"Attempted to play non-existent audio effect {effect}.");
            return;
        }
        m_source.PlayOneShot(soundEffects[effect]);
    }
}
