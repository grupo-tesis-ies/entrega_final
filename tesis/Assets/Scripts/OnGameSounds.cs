using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameSounds : MonoBehaviour {

    public static OnGameSounds instance = null;

    public AudioClip branchHitClip;

    public AudioClip loseCoinClip;

    public AudioClip coinClip;

    public AudioClip finishClip;

    public AudioClip berriesHitClip;

    public AudioClip powerUpClip;

    public AudioClip camHitClip;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    public void PlayBranchHit () {
        SoundManager.instance.PlaySingle (branchHitClip);
    }

    public void PlayLoseCoin () {
        SoundManager.instance.PlaySingle (loseCoinClip);
    }

    public void PlayGotCoin () {
        SoundManager.instance.PlaySingle (coinClip);
    }

    public void PlayLevelCompleted () {
        SoundManager.instance.PlaySingle (finishClip);
    }

    public void PlayBerriesHit () {
        SoundManager.instance.PlaySingle (berriesHitClip);
    }

    public void PlayCamHit() {
        SoundManager.instance.PlaySingle (camHitClip);
    }

    public void PlayGotPowerUp () {
        SoundManager.instance.PlaySingle (powerUpClip);
    }
}