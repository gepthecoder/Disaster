using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour {

    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;

    private Animator anim;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //where the player is
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
            //anim.SetBool("Walking", true);
        }
        else
        {
            nav.enabled = false;
            //anim.SetBool("Walking", false);
        }
    }
}
