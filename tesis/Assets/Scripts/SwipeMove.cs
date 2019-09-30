using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMove : MonoBehaviour {

    public float speed;
    private int mid;
    private float size;
 
    void Start()
    {
        mid = Screen.width / 2;
        size = Camera.main.orthographicSize / 2 - 0.1f;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            bool isLeft = Input.mousePosition.x < mid;
            if(!isLeft && transform.position.x < size)
            {
                transform.position += Vector3.right * Time.deltaTime * speed;
            }

            if (isLeft && transform.position.x > -size)
            {
                transform.position += Vector3.left * Time.deltaTime * speed;
            }
        }
    }
}
