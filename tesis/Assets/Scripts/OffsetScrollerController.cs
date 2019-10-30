using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScrollerController : MonoBehaviour {
    public static OffsetScrollerController instance = null;

    private float scrollSpeed;

    private Vector3 startPosition;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
        instance.startPosition = transform.position;
    }

    void FixedUpdate () {
        float newPosition = Mathf.Repeat (Time.time * instance.scrollSpeed, GameConstants.OBJECTS_OFFSET_TILE_SIZE);
        instance.transform.position = instance.startPosition + Vector3.down * newPosition;
    }

    public void SetSpeed (float speed) {
        instance.scrollSpeed = speed;
    }
}