using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgroDetection : MonoBehaviour {

    public event Action<Transform> OnAggro = delegate { };
    //private EnemyHealth enemyHealth;

    //private void Awake()
    //{
        //enemyHealth = GetComponent<EnemyHealth>();
    //}

	private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();
        var playerHealth = other.GetComponent<PlayerHealth>();


        if(player != null)
        {
            if(playerHealth.currentHealth > 0/* && enemyHealth.currentHealth > 0*/)
            {
                OnAggro(player.transform);
            }
           
            Debug.Log("Player Entered Danger Zone!"); 
        }
    }
}
