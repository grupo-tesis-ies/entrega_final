using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerController : MonoBehaviour
{
    private float scrollSpeed;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, GameConstants.OBJECTS_OFFSET_TILE_SIZE);
        transform.position = startPosition + Vector3.down * newPosition;
    }

    public void SetSpeed(float speed)
    {
        this.scrollSpeed = speed;
    }
}
