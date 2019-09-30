using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public string animation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<MainCharacterController>().SetPowerUp(animation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Obstacle" || other.tag == "Coin")
        {
            transform.position = new Vector3(Random.Range(-0.8f, 0.8f), 2.5f, transform.position.z);
        }
    }
}
