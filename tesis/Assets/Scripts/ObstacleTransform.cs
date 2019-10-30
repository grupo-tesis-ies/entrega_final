using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTransform : MonoBehaviour {
    public void RandomizeZ (bool isLeft) {
        float randomZrotation = Random.Range (-10f, 10f);
        transform.rotation = Quaternion.Euler (0, 0, randomZrotation);
    }
}