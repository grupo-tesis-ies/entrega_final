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

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    public void PlayBranchHit () {
        branchHitClip.Play();
    }

    public void PlayLoseCoin () {
        loseCoinClip.Play();
    }

    public void PlayGotCoin () {
        coinClip.Play();
    }

    public void PlayLevelCompleted () {
        finishClip.Play();
    }

    public void PlayBerriesHit () {
        berriesHitClip.Play();
    }

    public void PlayCamHit() {
        camHitClip.Play();
    }

    public void PlayGotPowerUp () {
        powerUpClip.Play();
    }
}