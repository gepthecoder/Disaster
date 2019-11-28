using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField]
    private float attackRefreshRate = 1.5f;

    [SerializeField]
    private AudioClip attackSound;

    private float attackTimer;
    private Animator animator;

    public Transform player;
    public float playerDistance;
    public float rotationDamping;
    public float moveSpeed;
    public static bool isPlayerAlive = true;

    private AudioSource sound;

    private EnemyHealth health; // for tracking its own health

    private PlayerHealth playerHealth;

    public AudioClip shoutAudio;
    private AudioSource audioSource;

    private bool isChasing = false;

    private float timeR = 5f;

    void Awake() {
        animator = GetComponentInChildren<Animator>();
        sound = GetComponent<AudioSource>();
        health = GetComponent<EnemyHealth>();

        FindPlayer();

        playerHealth = player.GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
    }
    
    void FindPlayer()
    {
        if (!EnterVehicle.playerInVehicle())
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("PlayerBackup").transform;
        }
        
    }

    // Update is called once per frame
    void Update() {

        timeR -= Time.deltaTime;

        if(timeR <= 0f)
        {
            timeR = 10f;
            FindPlayer();
        }

        attackTimer += Time.deltaTime;
        
        if (isPlayerAlive) {
            playerDistance = Vector3.Distance(player.position, transform.position);
            if (playerDistance < 5000f) {
                lookAtPlayer();
            }
            if (playerDistance < 4090f) {
                if (playerDistance > 2.5f) {
                    chase();
                    //if(playerDistance <= 20 && playerDistance >= 17) {
                    //    IwantToScreemAndShoutAndLetItAllOut();
                    //}
                } else if (playerDistance < 2.5f) {
                    if (attackTimer >= attackRefreshRate)
                        Attack();
                } else { isChasing = false; }
            }
        }
    }

    void lookAtPlayer() {

        Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }

    float SetSpeed() {
        float speed;

        if (isChasing) { speed = 1; }
        else { speed = 0; }

        return speed;
    }

    void chase() {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        isChasing = true;
        float CurrentSpeed = SetSpeed();
        animator.SetFloat("Speed", CurrentSpeed);
    }

    //void IwantToScreemAndShoutAndLetItAllOut() {
    //    transform.Translate(Vector3.forward * 0 * Time.deltaTime);
    //    //play audio for shouting
    //    audioSource.PlayOneShot(shoutAudio);
    //    animator.SetTrigger("Shout");
    //}

    private void Attack() {
        attackTimer = 0;

        sound.clip = attackSound;
        sound.Play();

        if (transform.gameObject.tag == "Enemy") 
        {
            playerHealth.TakeDamage(5,2);
            animator.SetTrigger("CanAttack");
        }
        if (transform.gameObject.tag == "Beast") 
        {
            playerHealth.TakeDamage(10,4);
            animator.SetTrigger("CanAttack");
            Debug.Log("Beast attacked!");
        }
       
        
    }
}
