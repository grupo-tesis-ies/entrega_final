using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject coin;
    public GameObject[] powerUps;
    public float instantiateTime;

    public void StartPlaying()
    {
        Invoke("InstantiateObs", .8f);
        Invoke("InstantiateCoin", .5f);
        Invoke("InstantiatePowerUps", 5f);
    }

    void InstantiateObs()
    {
        float randomTime = Random.Range(1.0f, 1.3f);
        int index = (int) Random.Range(0, obstacles.Length - 0.1f);
        Instantiate(obstacles[index]);
        Invoke("InstantiateObs", randomTime);
    }

    public void InstantiateCoin()
    {
        float randomTime = Random.Range(0.5f, 0.8f);
        Instantiate(coin);
        coin.transform.position = new Vector3(Random.Range(-0.8f, 0.8f), 2.5f, coin.transform.position.z);
        Invoke("InstantiateCoin", randomTime);
    }

    void InstantiatePowerUps()
    {
        float randomTime = Random.Range(10.0f, 17.0f);
        int index = (int)Random.Range(0, 2.9f);
        GameObject toInstantiate = powerUps[index];
        Instantiate(toInstantiate);
        toInstantiate.transform.position = new Vector3(Random.Range(-0.8f, 0.8f), 2.5f, toInstantiate.transform.position.z);
        Invoke("InstantiatePowerUps", randomTime);
    }
}
