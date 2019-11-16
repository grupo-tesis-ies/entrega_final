using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBodyController : MonoBehaviour {
    
    public static BirdBodyController instance = null;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    public void TriggerPowerUp (string powerUpName) {
        instance.GetComponent<Animator> ().SetTrigger (powerUpName);
    }
}