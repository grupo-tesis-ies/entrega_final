using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource[] sources;

    public AudioSource backgroundSource;

    public static SoundManager instance = null;

    private bool isPlaying = true;

    private bool isPlayingBackground = true;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
        DontDestroyOnLoad (gameObject);
    }

    public void PlaySingle (AudioClip clip) {
        if (!instance.isPlaying) {
            return;
        }

        foreach (AudioSource source in sources) {
            if (!source.isPlaying) {
                source.clip = clip;
                source.Play ();
                break;
            }
        }
    }

    public void RandomizeSfx (params AudioClip[] clips) {
        if (!instance.isPlaying) {
            return;
        }

        int randomIndex = Random.Range (0, clips.Length);
        PlaySingle (clips[randomIndex]);
    }

    public bool TriggerMusic () {
        instance.isPlaying = !instance.isPlaying;
        return instance.isPlaying;
    }

    public bool IsMusicOn () {
        return instance.isPlaying;
    }

    public bool IsBackgroundMusicOn () {
        return backgroundSource.volume > 0f;
    }

    public void StartMusic () {
        if (!backgroundSource.isPlaying) {
            backgroundSource.Play ();
        }
        if (backgroundSource.volume == 0f && instance.isPlayingBackground) {
            backgroundSource.volume = 0.1f;
        }
    }

    public bool TriggerBackgroundMusic () {
        instance.isPlayingBackground = !instance.isPlayingBackground;

        if (!instance.isPlayingBackground) {
            backgroundSource.volume = 0f;
        } else {
            backgroundSource.volume = 0.1f;
        }

        return instance.isPlayingBackground;
    }
}