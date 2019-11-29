using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteController : MonoBehaviour
{
    public static WhiteController instance = null;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    public void Flash () {
        instance.GetComponent<Animator>().SetTrigger (GameConstants.ANIMATION_FLASH);
    }
}
