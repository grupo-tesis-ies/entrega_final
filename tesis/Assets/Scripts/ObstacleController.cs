using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float obstacleSpeed;

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * obstacleSpeed);
    }
}
