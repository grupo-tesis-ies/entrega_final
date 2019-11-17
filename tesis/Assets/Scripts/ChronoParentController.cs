using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronoParentController : MonoBehaviour
{
    private void OnTriggerStay (Collider other) {
        if (GameConstants.TAG_OBSTACLE.Equals (other.tag) && transform.position.y > 2f) {
            transform.position = new Vector3 (Random.Range (-0.8f, 0.8f), GameConstants.OBJECTS_SPAWN_HEIGHT, transform.position.z);
        }
    }
}
