using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public string powerUpName;

    private void OnTriggerEnter(Collider other)
    {
        if (GameConstants.TAG_PLAYER.Equals(other.tag))
        {
            other.GetComponent<MainCharacterController>().SetPowerUp(powerUpName);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameConstants.TAG_OBSTACLE.Equals(other.tag) || GameConstants.TAG_COIN.Equals(other.tag))
        {
            transform.position = new Vector3(Random.Range(-0.8f, 0.8f), GameConstants.OBJECTS_SPAWN_HEIGHT, transform.position.z);
        }
    }
}
