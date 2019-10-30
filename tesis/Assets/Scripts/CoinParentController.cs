using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinParentController : MonoBehaviour {
    private void OnTriggerStay (Collider other) {
        if (GameConstants.TAG_OBSTACLE.Equals (other.tag) && !other.GetComponent<Obstacle>().IsDropping()) {
            transform.position = new Vector3 (Random.Range (-0.8f, 0.8f), Random.Range (GameConstants.OBJECTS_SPAWN_HEIGHT, GameConstants.OBJECTS_SPAWN_HEIGHT + 3f), transform.position.z);
        } else if (GameConstants.TAG_COIN.Equals (other.tag)) {
            Destroy (gameObject);
        }
    }
}