using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 50;
    public int currentHealth;
    public float sinkSpeed = 2.5f; //sink through the floor
    public int scoreValue = 10;
    public AudioClip deathClip;

    //references
    Animator anim;
    AudioSource enemyAudio;
    //ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    EnemyAI enemyAI;

    bool isDead;
    bool isSinking;

    public GameObject COIN;


    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        //hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyAI = GetComponent<EnemyAI>();

        currentHealth = startingHealth;

        PopUpTextContollerScript.Initialize();
    }


    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime); //move dead enemy underground RIP
        }
    }


    public void TakeDamage(int amount/*, Vector3 hitPoint*/)
    {
        if (isDead) { return; }
        else
        {
            enemyAudio.Play();

            currentHealth -= amount;

            PopUpTextContollerScript.CreateFloatingText(amount.ToString(), transform);

            if (currentHealth <= 0)
            {
                Death();
            }
        }          
    }

    void SetEnemyCenterPosition()
    {
        capsuleCollider.center.Set(0, -1, 0);
    }

    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;
        
        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        SetEnemyCenterPosition();

        anim.SetTrigger("Dead");
        EnduranceEffectManager.KillConfirmed = true;
        DisableEffects();
        if (gameObject.tag == "Beast")
        {
            WaveSpawner.BeastsLeft--;
            Level1Platform.overAllBossKills++;
            Level1Platform.remainingBossKills--;

            Vector3 currentPos = transform.position;

            CoinExplosion(10, currentPos, 4f);

        }
        else if (gameObject.tag == "Enemy")
        {
            WaveSpawner.enemiesLeft--;
            Level1Platform.overAllKills++;
            Level1Platform.remainingKills--;

            Vector3 currentPos = transform.position;

            CoinExplosion(5, currentPos, 4f);

        }

        StartCoroutine(StartSinking());


        //StartSinking();
        //KillManager.kills++       
    }

    private void DisableEffects()
    {
        enemyAI.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    //function call is inside enemy death animator
    public IEnumerator StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

        yield return new WaitForSeconds(3);
        isSinking = true;

        Destroy(gameObject, 2f);

        WaveSpawner.scorePoints += scoreValue;

        yield break;
    }

    public void CoinExplosion(int numOfCoins, Vector3 fromWhere, float power)
    {
        for (int i = 0; i < numOfCoins; i++)
        {
            GameObject coin = Instantiate(COIN, fromWhere, Quaternion.identity);
            if(coin.GetComponent<Rigidbody>() != null)
            {
                float randomX = Random.Range(0.1f, 0.5f);
                coin.GetComponent<Rigidbody>().AddForce(transform.up + new Vector3(randomX, 0, 0) * power);
            }
        }
    }
}
