using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityBase.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : SerializedMonoBehaviour
{
    public static AudioManager Instance { get; set; } // Singleton boilerplate
    [Range(0, 1)] public float musicVolume; // Max volume for music source
    [Range(0, 1)] public float sfxVolume; // Max volume for sfx
    public AudioSource musicSource, sfxSource; // Audio sources
    public AudioClip[] sceneMusic; // Music array
    public Dictionary<string, AudioClip> soundEffects;

    int m_curPlayingIdx = -1; // Index of currently playing song.

    void Awake()
    {
        // Singleton boilerplate
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // We change music between levels, so we need to know when a new level is loaded
        SceneManager.activeSceneChanged += ChangeMusic;

        // Make sure we stay active between scenes, otherwise we cannot
        DontDestroyOnLoad(gameObject);

        // Reset the volumes for our audio sources.
        if (musicSource == null || sfxSource == null) return;
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    // Called when the scene changes. changes the music to the corresponding scene's music.
    void ChangeMusic(Scene current, Scene next)
    {
        if (musicSource == null) return; // Early out if we don't have a source for music
        musicSource.volume = musicVolume; // Reset volume
        if (m_curPlayingIdx == next.buildIndex) return; // Don't change the music if we reloaded the same level
        if (next.buildIndex >= sceneMusic.Length) return; // Don't change if there is no music for the current level
        m_curPlayingIdx = next.buildIndex; // Remember our current level
        
        // Play the new music
        musicSource.clip = sceneMusic[next.buildIndex];
        musicSource.Play();
    }

    // Static wrapper for the Play method
    public static void PlayEffect(string effect) => Instance.Play(effect);

    // Play a sound effect by id
    public void Play(string effect)
    {
        // If there are no effects or source, skip
        if (soundEffects == null || sfxSource == null) return;
        if (!soundEffects.ContainsKey(effect))
        {
            // Warn if there is no matching effect
            Debug.LogWarning($"Attempted to play non-existent audio effect {effect}.");
            return;
        }

        // Create a new AudioSource, based on the sfxSource prefab.
        var newSfx = Instantiate(sfxSource, transform);
        newSfx.clip = soundEffects[effect];
        newSfx.Play();

        // Make sure to clean up after us :)
        // Kill the copy after the sound has finished (based on timing)
        Destroy(newSfx.gameObject, newSfx.clip.length + 0.1f);
    }

    // Fade the music volume over time.
    public void Fade(float time)
    {
        if (musicSource == null) return; // Skip if there is no music source.

        // Animate the volume to 0, using the Step4 function. Basically goes in 5 distinct steps : (1, 0.75, 0.5, 0.25, 0)
        musicSource.AnimateVolume(0, time, EaseMode.Step4);
    }
}
