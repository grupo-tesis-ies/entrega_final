using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMove : MonoBehaviour {

    public float speed;
    private int mid;
    private float size;
    private float maxSpeed;
    private bool isInverted;

    private float minSpeed;

    private float maxTopSpeed;

    void Start () {
        mid = Screen.width / 2;
        size = Camera.main.orthographicSize / 2 - 0.1f;
        maxSpeed = speed;
        minSpeed = maxSpeed / 4;
        isInverted = false;
        maxTopSpeed = maxSpeed;
    }

    void Update () {
        if (Input.GetMouseButton (0)) {
            //transform.position.x = Input.mousePosition.x;
            Camera cam = Camera.main;
            //Input.GetTouch(0)
            //transform.position = new Vector3(, transform.position.y, transform.position.z);
            bool isLeft = Input.mousePosition.x < mid;
            if (isInverted) {
                if (isLeft) {
                    transform.position += Vector3.right * Time.deltaTime * speed;
                } else {
                    transform.position += Vector3.left * Time.deltaTime * speed;
                }
                transform.position = new Vector3 (Mathf.Clamp (transform.position.x, -0.8f, 0.8f), transform.position.y, transform.position.z);
            } else {
                float difference = transform.position.x - cam.ScreenToWorldPoint (Input.mousePosition).x;
                Vector3 direction = difference > 0f ? Vector3.left : Vector3.right;

                if (Mathf.Abs (difference) > 0.2f) {
                    speed += 1f;
                    speed = Mathf.Clamp (speed, maxSpeed / 4, maxSpeed);
                    transform.position = Vector3.Lerp (transform.position, new Vector3 (cam.ScreenToWorldPoint (Input.mousePosition).x, transform.position.y, transform.position.z), Time.deltaTime * speed);
                } else if (Mathf.Abs (difference) > 0.1f) {
                    speed = maxSpeed / 4;
                    transform.position = Vector3.Lerp (transform.position, new Vector3 (cam.ScreenToWorldPoint (Input.mousePosition).x, transform.position.y, transform.position.z), Time.deltaTime * speed);
                }
            }

            //    if(!isLeft && transform.position.x < size)
            //    {
            //        float minRotationZ = 0f;
            //        float maxRotationZ = 18f;
            //        float minRotationY = -180f;
            //        float maxRotationY = -200f;
            //        transform.Rotate(new Vector3(0, -1, 1), 2f);
            //        //transform.Rotate(Vector3.up, -2f);
            //        Vector3 currentRotation = transform.localRotation.eulerAngles;
            //        currentRotation.z = Mathf.Clamp(currentRotation.z, minRotationZ, maxRotationZ);
            //        currentRotation.y = Mathf.Clamp(currentRotation.y, minRotationY, maxRotationY);
            //        transform.Rotate(currentRotation);
            //        //transform.localRotation = Quaternion.Euler(currentRotation);
            //        transform.position += Vector3.right * Time.deltaTime * speed;
            //    }

            //    if (isLeft && transform.position.x > -size)
            //    {
            //        float minRotationZ = 340f;
            //        float maxRotationZ = 360f;
            //        float minRotationY = -180f;
            //        float maxRotationY = -160f;
            //        transform.Rotate(new Vector3(0, -1, 1), -2f);
            //        //transform.Rotate(Vector3.forward, -2f);
            //        //transform.Rotate(Vector3.up, 2f);

            //        Vector3 currentRotation = transform.localRotation.eulerAngles;
            //        currentRotation.z = Mathf.Clamp(currentRotation.z, minRotationZ, maxRotationZ);
            //        currentRotation.y = Mathf.Clamp(currentRotation.y, minRotationY, maxRotationY);
            //        //transform.localRotation = Quaternion.Euler(currentRotation);
            //        transform.Rotate(currentRotation);
            //        transform.position += Vector3.left * Time.deltaTime * speed;
            //    }
            //}
            //else
            //{
            //    Vector3 currentRotation = transform.localRotation.eulerAngles;
            //    if (currentRotation.z >= 18f)
            //    {
            //        transform.Rotate(Vector3.forward, -0.5f);
            //        transform.Rotate(Vector3.up, 0.5f);
            //        currentRotation.z = Mathf.Clamp(currentRotation.z, 18f, 0f);
            //        currentRotation.y = Mathf.Clamp(currentRotation.y, -200f, -180f);
            //        transform.localRotation = Quaternion.Euler(currentRotation);
            //    }
            //    else
            //    {
            //        transform.Rotate(Vector3.back, -0.5f);
            //        transform.Rotate(Vector3.up, -0.5f);
            //        currentRotation.z = Mathf.Clamp(currentRotation.z, 360f, 340f);
            //        currentRotation.y = Mathf.Clamp(currentRotation.y, -160f, -180f);
            //        transform.localRotation = Quaternion.Euler(currentRotation);
            //    }
        }
    }

    public void SlowDownHor () {
        maxSpeed = minSpeed;
        StartCoroutine (ReturnSpeed ());
    }

    IEnumerator ReturnSpeed () {
        yield return new WaitForSeconds (5);
        maxSpeed = maxTopSpeed;
    }

    public void Invert () {
        isInverted = true;
        StartCoroutine (InvertOff ());

    }

    IEnumerator InvertOff () {
        yield return new WaitForSeconds (5);
        isInverted = false;
    }
}