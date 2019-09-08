using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Coin")
        {
            if(other.tag == "Player") {
                GameObject.Find("Score Manager").GetComponent<ScoreManager>().AddCoin();
            }
            Destroy(gameObject);
        }
    }
}
