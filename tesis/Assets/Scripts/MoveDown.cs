using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    private float speed;

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }

    public void SetMovingSpeed(float speed)
    {
        this.speed = speed;
    }
}
