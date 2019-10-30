using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBoard : MonoBehaviour {

    public static TitleBoard instance = null;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    public void TriggerTitleBoard () {
        instance.GetComponent<Animator>().SetTrigger (GameConstants.ANIMATION_TITLE_BOARD_DISPLAY);
    }

    public void TriggerWords () {
        GameEvents.instance.TriggerTitleWords ();
    }
}