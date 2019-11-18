using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsFactory : MonoBehaviour {

    public static ObjectsFactory instance = null;
    public GameObject[] obstacles;
    public GameObject[] powerUps;
    public GameObject coin;
    public GameObject chrono;

    private float speed;

    private bool isProducing = true;
    private GameObject lastObstacle;

    void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    public void InstantiateObs () {
        float randomTime = Random.Range (GameParameters.instance.GetObsMinSpawnTime (), GameParameters.instance.GetObsMaxSpawnTime ());

        if (isProducing) {
            int index;
            GameObject toInstantiate;

            index = (int) Random.Range (0, obstacles.Length - 0.1f);
            toInstantiate = obstacles[index];

            if (lastObstacle != null) {
                do {
                    index = (int) Random.Range (0, obstacles.Length - 0.1f);
                    toInstantiate = obstacles[index];
                } while (toInstantiate.transform.GetChild (0).gameObject.GetComponent<Obstacle> ().GetName ().Equals (lastObstacle.transform.GetChild (0).gameObject.GetComponent<Obstacle> ().GetName ()));
            }

            GameObject ob = Instantiate (toInstantiate);
            lastObstacle = ob;

            ob.GetComponent<MoveDown> ().SetMovingSpeed (instance.speed);

            bool isLeft = Random.value >= 0.5f;
            GameObject child = ob.transform.GetChild (0).gameObject;
            if (child.GetComponent<ObstacleTransform> () != null) {
                child.GetComponent<ObstacleTransform> ().RandomizeZ (isLeft);
            }

            if (ob.tag != "Cam_tmp") {
                if (!isLeft) {
                    ob.transform.position = new Vector3 (ob.transform.position.x * -1, ob.transform.position.y, ob.transform.position.z);
                    if (GameConstants.OBSTACLE_THORN.Equals (child.GetComponent<Obstacle> ().GetName ())) {
                        child.transform.rotation = Quaternion.Euler (-90, -180, 0);
                    } else {
                        child.transform.rotation = Quaternion.Euler (0, -180, 0);
                    }
                }
            }
        }

        Invoke ("InstantiateObs", randomTime);
    }

    public void InstantiateCoin () {
        float randomTime = Random.Range (GameParameters.instance.GetCoinsMinSpawnTime (), GameParameters.instance.GetCoinsMaxSpawnTime ());

        if (isProducing) {
            GameObject instantiated = Instantiate (coin);
            instantiated.transform.position = new Vector3 (Random.Range (-0.8f, 0.8f), 2.5f, instantiated.transform.position.z);
            instantiated.GetComponent<MoveDown> ().SetMovingSpeed (instance.speed);
        }

        Invoke ("InstantiateCoin", randomTime);
    }

    public void InstantiatePowerUps () {
        float randomTime = Random.Range (GameParameters.instance.GetPowerUpMinSpawnTime (), GameParameters.instance.GetPowerUpMaxSpawnTime ());

        if (isProducing) {
            int index = (int) Random.Range (0, powerUps.Length - 0.1f);
            GameObject instantiated = Instantiate (powerUps[index]);
            instantiated.transform.position = new Vector3 (Random.Range (-0.8f, 0.8f), 2.5f, instantiated.transform.position.z);
            instantiated.GetComponent<MoveDown> ().SetMovingSpeed (instance.speed);
        }

        Invoke ("InstantiatePowerUps", randomTime);
    }

    public void InstantiateChrono () {
        float randomTime = Random.Range (GameParameters.instance.GetChronoMinSpawnTime (), GameParameters.instance.GetChronoMaxSpawnTime ());

        if (isProducing) {
            GameObject instantiated = Instantiate (chrono);
            instantiated.transform.position = new Vector3 (Random.Range (-0.8f, 0.8f), 2.5f, instantiated.transform.position.z);
            instantiated.GetComponent<MoveDown> ().SetMovingSpeed (instance.speed);
        }

        Invoke ("InstantiateChrono", randomTime);
    }

    public void SetSpeed (float obstacleSpeed) {
        instance.speed = obstacleSpeed;
        foreach (MoveDown moveDown in Resources.FindObjectsOfTypeAll (typeof (MoveDown))) {
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