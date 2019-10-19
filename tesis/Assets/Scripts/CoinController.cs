using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public GameEvents gameEvents;

    private void OnTriggerEnter(Collider other)
    {
        if (GameConstants.TAG_PLAYER.Equals(other.tag))
        {
            gameEvents.CoinTriggered();
            Destroy(gameObject);
        }
    }
}
