using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinParentController : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == GameConstants.TAG_OBSTACLE)
        {
            transform.position = new Vector3(Random.Range(-0.8f, 0.8f), GameConstants.OBJECTS_SPAWN_HEIGHT, transform.position.z);
        }
    }
}
