using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {
    public string powerUpName;

    private void OnTriggerEnter (Collider other) {
        if (GameConstants.TAG_PLAYER.Equals (other.tag)) {
            GameEvents.instance.PowerUpTriggered(powerUpName);
            Destroy (gameObject);
        }
    }
}