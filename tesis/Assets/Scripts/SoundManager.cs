using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource[] sources;

    public static SoundManager instance = null;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
        DontDestroyOnLoad (gameObject);
    }

    public void PlaySingle (AudioClip clip) {
        foreach (AudioSource source in sources) {
            if (!source.isPlaying) {
                source.clip = clip;
                source.Play ();
                break;
            }
        }
    }

    public void RandomizeSfx (params AudioClip[] clips) {
        int randomIndex = Random.Range (0, clips.Length);
        PlaySingle (clips[randomIndex]);
    }
}