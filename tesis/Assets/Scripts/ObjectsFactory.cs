using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsFactory : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject[] powerUps;
    public GameObject coin;

    private float obstacleSpeed = 2f;

    public void InstantiateObs()
    {
        float randomTime = Random.Range(1.0f, 1.3f);
        
        int index = (int) Random.Range(0, obstacles.Length - 0.1f);
        GameObject ob = Instantiate(obstacles[index]);
        ob.GetComponent<MoveDown>().SetMovingSpeed(obstacleSpeed);
        GameObject obstacle = ob.transform.GetChild(0).gameObject;
        bool isLeft = Random.value >= 0.5f;

        float randomZrotation = Random.Range(-10f, 10f);
        if (!isLeft)
        {
            obstacle.transform.position = new Vector3(obstacle.transform.position.x * -1, obstacle.transform.position.y, obstacle.transform.position.z);
            obstacle.transform.rotation = Quaternion.Euler(0, -180, randomZrotation);
        } else
        {
            obstacle.transform.rotation = Quaternion.Euler(0, 0, randomZrotation);
        }

        Invoke("InstantiateObs", randomTime);
    }

    public void InstantiateCoin()
    {
        float randomTime = Random.Range(0.5f, 0.8f);
        GameObject instantiated = Instantiate(coin);
        instantiated.transform.position = new Vector3(Random.Range(-0.8f, 0.8f), 2.5f, instantiated.transform.position.z);
        instantiated.GetComponent<MoveDown>().SetMovingSpeed(obstacleSpeed);
        Invoke("InstantiateCoin", randomTime);
    }

    public void InstantiatePowerUps()
    {
        float randomTime = Random.Range(10.0f, 17.0f);
        int index = (int)Random.Range(0, powerUps.Length - 0.1f);
        GameObject instantiated = Instantiate(powerUps[index]);
        instantiated.transform.position = new Vector3(Random.Range(-0.8f, 0.8f), 2.5f, instantiated.transform.position.z);
        instantiated.GetComponent<MoveDown>().SetMovingSpeed(obstacleSpeed);
        Invoke("InstantiatePowerUps", randomTime);
    }
}
