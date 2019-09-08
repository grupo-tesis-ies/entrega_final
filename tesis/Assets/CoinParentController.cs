using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinParentController : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Obstacle")
        {
            transform.position = new Vector3(Random.Range(-0.8f, 0.8f), 2.5f, transform.position.z);
        }
    }
}
