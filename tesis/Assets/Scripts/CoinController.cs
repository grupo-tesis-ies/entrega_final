using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {
    private void OnTriggerEnter (Collider other) {
        if (GameConstants.TAG_PLAYER.Equals (other.tag)) {
            GameEvents.instance.CoinTriggered ();
            Destroy (gameObject);
        }
    }
}