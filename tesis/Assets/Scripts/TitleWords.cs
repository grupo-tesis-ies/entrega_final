using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleWords : MonoBehaviour {
    public static TitleWords instance = null;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    public void TriggerWords () {
        instance.GetComponent<Animator>().SetTrigger (GameConstants.ANIMATION_TITLE_WORDS_DISPLAY);
    }

    public void WordsFinished() {
        MainMenuManager.instance.TriggeredWords();
    }
}