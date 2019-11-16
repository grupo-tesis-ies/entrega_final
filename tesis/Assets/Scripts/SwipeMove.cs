using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeMove : MonoBehaviour {

    public static SwipeMove instance = null;

    private float speed;
    private float actualSpeed;
    private float minSpeed;

    private int mid;
    private float size;

    private float rotationZ = 0f;
    private float rotationY = 180f;
    private float minRotationZ = -15f;
    private float maxRotationZ = 15f;

    private float minRotationY = -210f;
    private float maxRotationY = -150f;

    private bool isSlowed;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    void Start () {
        mid = Screen.width / 2;
        size = Camera.main.orthographicSize / 2 - 0.1f;
    }

    void Update () {
        instance.speed = GameParameters.instance.GetBirdSpeed ();
        instance.minSpeed = speed / 4;

        if (!isSlowed) {
            instance.actualSpeed = speed;
        }

        if (Input.GetMouseButton (0)) {
            bool isLeft = Input.mousePosition.x < mid;

            if (!isLeft && transform.position.x < size) {
                transform.position += Vector3.right * Time.deltaTime * actualSpeed;
                rotationZ += Time.deltaTime * actualSpeed * 40f;
                rotationY -= Time.deltaTime * actualSpeed * 40f;
            } else if (isLeft && transform.position.x > -size) {
                rotationZ -= Time.deltaTime * actualSpeed * 40f;
                rotationY += Time.deltaTime * actualSpeed * 40f;
                transform.position += Vector3.left * Time.deltaTime * actualSpeed;
            }
        } else {
            rotationZ = Mathf.InverseLerp (rotationZ, 0f, 2f);
            rotationY = Mathf.Lerp (rotationY, -180f, 2f);
        }

        rotationZ = Mathf.Clamp (rotationZ, minRotationZ, maxRotationZ);
        rotationY = Mathf.Clamp (rotationY, minRotationY, maxRotationY);
        transform.localRotation = Quaternion.Euler (0, rotationY, rotationZ);
    }

    public void SetSlowed () {
        instance.actualSpeed = minSpeed;
        instance.isSlowed = true;
    }

    public void ReturnSpeed () {
        instance.isSlowed = false;
    }
}