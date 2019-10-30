using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour {
    private void OnTriggerEnter (Collider other) {
        if (other.transform.parent != null) {
            Destroy (other.transform.parent.gameObject);
        }

        Destroy (other.gameObject);
    }
}