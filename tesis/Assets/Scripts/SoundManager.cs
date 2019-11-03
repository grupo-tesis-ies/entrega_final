using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource[] sources;

    public static SoundManager instance = null;

    private bool isPlaying = true;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
        DontDestroyOnLoad (gameObject);
    }

    public void PlaySingle (AudioClip clip) {
        if(!instance.isPlaying) {
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
        if(!instance.isPlaying) {
            return;
        }

        int randomIndex = Random.Range (0, clips.Length);
        PlaySingle (clips[randomIndex]);
    }

    public bool TriggerMusic () {
        instance.isPlaying = !instance.isPlaying;
        return instance.isPlaying;
    }
}