using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource backgroundEfx;
    public AudioSource powerUpsEfx;

    public static SoundManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        backgroundEfx.clip = clips[randomIndex];
        backgroundEfx.Play();
    }

    public void PlayPowerUp(AudioClip clip)
    {
        powerUpsEfx.clip = clip;
        powerUpsEfx.Play();
    }
}
