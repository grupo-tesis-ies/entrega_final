using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject coin;
    public float instantiateTime;

    public void StartPlaying()
    {
        Invoke("InstantiateObs", .8f);
        Invoke("InstantiateCoin", .5f);
    }

    void InstantiateObs()
    {
        float randomTime = Random.Range(1.0f, 1.3f);
        int index = (int) Random.Range(0, 11.5f);
        Instantiate(obstacles[index]);
        Invoke("InstantiateObs", randomTime);
    }

    public void InstantiateCoin()
    {
        float randomTime = Random.Range(1.0f, 1.2f);
        Instantiate(coin);
        coin.transform.position = new Vector3(Random.Range(-0.8f, 0.8f), 2.5f, coin.transform.position.z);
        Invoke("InstantiateCoin", randomTime);
    }
}
