using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsFactory : MonoBehaviour {

    public static ObjectsFactory instance = null;
    public GameObject[] obstacles;
    public GameObject[] powerUps;
    public GameObject coin;

    private float speed = 2f;

    private bool isProducing = true;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    public void InstantiateObs () {
        float randomTime = Random.Range (1.0f, 1.3f);

        if (isProducing) {
            int index = (int) Random.Range (0, obstacles.Length - 0.1f);
            GameObject ob = Instantiate (obstacles[index]);
            ob.GetComponent<MoveDown> ().SetMovingSpeed (speed);

            bool isLeft = Random.value >= 0.5f;
            GameObject child = ob.transform.GetChild (0).gameObject;
            if (child.GetComponent<ObstacleTransform> () != null) {
                child.GetComponent<ObstacleTransform> ().RandomizeZ (isLeft);
            }

            if (ob.tag != "Cam_tmp") {
                if (!isLeft) {
                    ob.transform.position = new Vector3 (ob.transform.position.x * -1, ob.transform.position.y, ob.transform.position.z);
                    child.transform.rotation = Quaternion.Euler (0, -180, 0);
                }
            }
        }

        Invoke ("InstantiateObs", randomTime);
    }

    public void InstantiateCoin () {
        float randomTime = Random.Range (0.5f, 0.8f);

        if (isProducing) {
            GameObject instantiated = Instantiate (coin);
            instantiated.transform.position = new Vector3 (Random.Range (-0.8f, 0.8f), 2.5f, instantiated.transform.position.z);
            instantiated.GetComponent<MoveDown> ().SetMovingSpeed (speed);
        }

        Invoke ("InstantiateCoin", randomTime);
    }

    public void InstantiatePowerUps () {
        float randomTime = Random.Range (10.0f, 17.0f);

        if (isProducing) {
            int index = (int) Random.Range (0, powerUps.Length - 0.1f);
            GameObject instantiated = Instantiate (powerUps[index]);
            instantiated.transform.position = new Vector3 (Random.Range (-0.8f, 0.8f), 2.5f, instantiated.transform.position.z);
            instantiated.GetComponent<MoveDown> ().SetMovingSpeed (speed);
        }

        Invoke ("InstantiatePowerUps", randomTime);
    }

    public void SetSpeed (float obstacleSpeed) {
        instance.speed = obstacleSpeed;
        foreach (MoveDown moveDown in GameObject.FindObjectsOfType (typeof (MoveDown))) {
            moveDown.SetMovingSpeed (obstacleSpeed);
        }
    }

    public void StopProducing () {
        instance.isProducing = false;
    }

    public void Produce () {
        instance.isProducing = true;
    }
}