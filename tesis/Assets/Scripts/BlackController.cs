using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackController : MonoBehaviour {

    public static BlackController instance = null;
    private Animator animator;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
        DontDestroyOnLoad (gameObject);
    }

    void Start () {
        FadeOut ();
    }

    void FadeFinish () {
        GameEvents.instance.FadeFinish ();
    }

    public void FadeOut () {
        instance.GetComponent<Animator>().SetTrigger (GameConstants.ANIMATION_BLACK_FADE_OUT);
    }

    public void FadeIn () {
        instance.GetComponent<Animator>().SetTrigger (GameConstants.ANIMATION_BLACK_FADE_IN);
    }

    void OnLevelWasLoaded (int level) {
        FadeOut ();
    }
}