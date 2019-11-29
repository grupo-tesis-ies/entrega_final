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

    void Update () {
        if (MainCharacterController.instance.IsImpulseUp () && transform.position.y < 2f) {
            transform.parent.GetComponent<MoveDown> ().enabled = false;
            transform.position = Vector3.MoveTowards (transform.position, MainCharacterController.instance.GetPosition () + (Vector3.up * .1f), 5 * Time.deltaTime);
        } else {
            transform.parent.GetComponent<MoveDown> ().enabled = true;
        }
    }
}