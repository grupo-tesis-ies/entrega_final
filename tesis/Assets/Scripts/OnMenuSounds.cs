using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMenuSounds : MonoBehaviour {
    public static OnMenuSounds instance = null;

    public AudioClip boardClip;

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
}