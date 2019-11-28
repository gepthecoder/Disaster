using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    private Animator animator;
    private AgroDetection aggroDetection;
    private NavMeshAgent navMeshAgent;

    private Transform target;

    private EnemyHealth health;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        aggroDetection = GetComponent<AgroDetection>();
        health = GetComponent<EnemyHealth>();
        aggroDetection.OnAggro += AggroDetection_OnAggro;
    }

    private void AggroDetection_OnAggro(Transform target)
    {
        this.target = target;
    }
    private bool IsNotDead()
    {
        return health.currentHealth > 0;
    }

    private void Update()
    {
        if(target != null && target.GetComponent<PlayerHealth>().currentHealth > 0 && IsNotDead())
        {
            navMeshAgent.SetDestination(target.position);
            float CurrentSpeed = navMeshAgent.velocity.magnitude;
            animator.SetFloat("Speed", CurrentSpeed);
        }
    }

    public float RemainingDistance()
    {
        float remainingDistance;

        remainingDistance = navMeshAgent.remainingDistance;

        return remainingDistance;
    }
}
