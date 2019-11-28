using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {


    public static bool playerWatchedAD_extraChance;
    public Text healthTrackerText;
    public int startingHealth = 100;
    public int startingShield = 10;

    public int currentHealth;
    public int currentShield;

    public Slider healthSlider;
    public Slider ShieldSlider;

    public AudioClip deathClip;
    public AudioClip playerHurt;


    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public float sinkSpeed = 2.5f;


    Animator anim;
    AudioSource playerAudio;
    myThirdPersonController controller;
    //PlayerMovement playerMovement;
    //Gun /*playerShooting*/;

    public bool isDead;
    bool damaged;
    bool isSinking;


    void Awake() {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        //playerMovement = GetComponent<PlayerMovement>();
        //playerShooting = GetComponentInChildren<Gun>();
        controller = GetComponent<myThirdPersonController>();

        currentHealth = startingHealth;
        currentShield = startingShield;
        //UIhealth.txtHealth = currentHealth;
        playerWatchedAD_extraChance = false;
    }


    void Update() {

        ShieldSlider.value = currentShield;
        healthSlider.value = currentHealth;

        healthTrackerText.text = healthSlider.value.ToString();
        //check if zombie attecked player then lerp red light on screen
        if (damaged) {
            damageImage.color = flashColour;
        } else {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        //check if it is time to sink player
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }

        if (playerWatchedAD_extraChance)
        {
            currentHealth = 20;
            currentShield = 2;
            EnemyAI.isPlayerAlive = true;
            isDead = false;

            playerWatchedAD_extraChance = false;
        }



    }

    public void TakeDamage(int amountH, int amountS) {
        damaged = true;

        if(currentShield > 0) { currentShield -= amountS; }
        else { currentHealth -= amountH; }
        //UIhealth.txtHealth = currentHealth;
        ShieldSlider.value = currentShield;
        healthSlider.value = currentHealth;
        playerAudio.clip = playerHurt;
        playerAudio.Play();

        if (currentHealth <= 0 && !isDead) {
            Death();
        }
    }


    void Death() {
        EnemyAI.isPlayerAlive = false;
        isDead = true;
        controller.enabled = false;
        anim.SetTrigger("death");
        playerAudio.PlayOneShot(deathClip);        
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator DeathSinkIn()
    {
        
        yield return new WaitForSeconds(4f);

        isSinking = true;

        yield break;
    }
}
