using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMenuSounds : MonoBehaviour {
    public static OnMenuSounds instance = null;

    public AudioClip boardClip;
    public AudioClip launchClip;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    public void PlayBoard () {
        SoundManager.instance.PlaySingle (boardClip);
    }

    public void PlayFlyLaunch() {
        SoundManager.instance.PlaySingle (launchClip);
    }
}