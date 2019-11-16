using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {

    public static CamController instance = null;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    void TriggerZoomOffFinish () {
        GameEvents.instance.ZoomOffFinish ();
    }

    public void TriggerSignIn () {
        instance.GetComponent<Animator> ().SetTrigger (GameConstants.ANIMATION_SIGN_IN);
    }

    public void TriggerSignInFinish () {
        TutorialController.instance.SignInFinish();
    }

    public void TriggerSignOut () {
        instance.GetComponent<Animator> ().SetTrigger (GameConstants.ANIMATION_SIGN_OUT);
    }

    public void TriggerZoomOut () {
        instance.GetComponent<Animator> ().SetTrigger (GameConstants.ANIMATION_ZOOM_OUT);
    }
}