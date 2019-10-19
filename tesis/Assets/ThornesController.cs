using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornesController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(GameConstants.TAG_PLAYER.Equals(other.tag))
        {
            GameObject.Find("MainCharacter").GetComponent<SwipeMove>().Invert();
        }
    }
}
