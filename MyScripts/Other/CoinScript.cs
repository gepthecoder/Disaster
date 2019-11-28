using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CoinManager.PickedUp = true;
            CoinManager.numOfCoins++;

            Destroy(gameObject);
        }   
    }
}
