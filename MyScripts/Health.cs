using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    [SerializeField]
    [Range(1,100)]
    private int startingHealth = 5;

    [SerializeField]
    private AudioSource hurtSound;

    [SerializeField]
    private ParticleSystem Explode;

    public int currentHealth;

    private Animator animator;

    private bool Dead;
    private EnemyMovement enemyMovement;
    private Enemy enemy;

    


    private void OnEnable()
    {
        currentHealth = startingHealth;
        animator = GetComponentInChildren<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemy = GetComponent<Enemy>();
        

        Dead = false;
    }

    public int CurrentHealthValue()
    {
        return currentHealth;
    }


    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        hurtSound.Play();
        if(currentHealth <= 0 && !Dead)
        {         
            Die();
        }
    }

    private void Die()
    {
        Dead = true;

        if (gameObject.tag == "Player")
        {
            animator.SetBool("IsDead", Dead);
        }
        else if(gameObject.tag == "Zombie")
        {
            if(Explode != null)
            {
                Explode.Play();
            }            
            animator.SetBool("DeadZombie", Dead);                                        
        }      
    }

}
