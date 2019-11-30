using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameSounds : MonoBehaviour {

    public static OnGameSounds instance = null;

    public AudioSource branchHitClip;

    public AudioSource loseCoinClip;

    public AudioSource coinClip;

    public AudioSource finishClip;

    public AudioSource berriesHitClip;

    public AudioSource powerUpClip;

    public AudioSource camHitClip;

    public AudioSource buttonSound;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    public void PlayBranchHit () {
        if (SoundManager.instance.IsMusicOn ()) {
            branchHitClip.Play ();
        }
    }

    public void PlayLoseCoin () {
        if (SoundManager.instance.IsMusicOn ()) {
            loseCoinClip.Play ();
        }
    }

    public void PlayGotCoin () {
        if (SoundManager.instance.IsMusicOn ()) {
            coinClip.Play ();
        }
    }

    public void PlayLevelCompleted () {
        if (SoundManager.instance.IsMusicOn ()) {
            finishClip.Play ();
        }
    }

    public void PlayBerriesHit () {
        if (SoundManager.instance.IsMusicOn ()) {
            berriesHitClip.Play ();
        }
    }

    public void PlayCamHit () {
        if (SoundManager.instance.IsMusicOn ()) {
            camHitClip.Play ();
        }
    }

    public void PlayGotPowerUp () {
        if (SoundManager.instance.IsMusicOn ()) {
            powerUpClip.Play ();
        }
    }

    public void PlayButton () {
        if (SoundManager.instance.IsMusicOn ()) {
            buttonSound.Play ();
        }
    }
}