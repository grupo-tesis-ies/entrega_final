using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseParentController : MonoBehaviour
{
    private void OnTriggerStay (Collider other) {
        if (GameConstants.TAG_OBSTACLE.Equals (other.tag) && !other.GetComponent<Obstacle>().IsDropping() && transform.position.y > 2f) {
            transform.position = new Vector3 (Random.Range (-0.8f, 0.8f), Random.Range (GameConstants.OBJECTS_SPAWN_HEIGHT, GameConstants.OBJECTS_SPAWN_HEIGHT + 3f), transform.position.z);
        }
    }
}
