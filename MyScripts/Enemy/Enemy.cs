using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private float attackRefreshRate = 1.5f;

    [SerializeField]
    private AudioClip attackSound;

    private AgroDetection aggroDetection;
    private PlayerHealth healthTarget;
    private NavMeshAgent navMeshAgent;

    private float attackTimer;
    private Animator animator;
    private AudioSource sound;

    private EnemyHealth health;

    public Collider colli;

    private bool canAttack = false;


    private void Awake() {
        aggroDetection = GetComponent<AgroDetection>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        sound = GetComponent<AudioSource>();
        health = GetComponent<EnemyHealth>();
        aggroDetection.OnAggro += AggroDetection_OnAggro;
    }

    private void AggroDetection_OnAggro(Transform target) {
        PlayerHealth health = target.GetComponent<PlayerHealth>();
        if (health != null) {
            healthTarget = health;
        }
    }


    // Update is called once per frame
    void Update() {

        attackTimer += Time.deltaTime;
        Debug.Log(canAttack);

        if (healthTarget != null) {
            if (attackTimer >= attackRefreshRate) {
                if (!PlayerDead()) {
                    if (EnemyInRange(canAttack)) {
                        Attack();
                    }
                }
            }
        }
    }



    void OnTriggerEnter(Collider collider) {
        collider = colli;
        if(collider.gameObject.tag == "Player") { canAttack = true; }

    }

    void OnTriggerExit(Collider collider) {
        collider = colli;
        if (collider.gameObject.tag == "Player") { canAttack = false; }

    }
    private bool EnemyInRange(bool canAttack)
    {
        return canAttack;
    }

    private bool PlayerDead()
    {
        return healthTarget.currentHealth <= 0;
    }

    private bool EnemyDead()
    {
        return health.currentHealth <= 0;
    }


    private void Attack()
    {
        attackTimer = 0;

        sound.clip = attackSound;
        sound.Play();
        
        healthTarget.TakeDamage(5,2);

        animator.SetTrigger("CanAttack");
    }
}
