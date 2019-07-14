using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    public GameObject[] obstacles;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstantiateObs", 0.5f, 1f);
    }

    void InstantiateObs()
    {
        
    }
}
