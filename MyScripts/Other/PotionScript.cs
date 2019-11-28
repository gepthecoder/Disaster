using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    public AudioClip clipHealth;
    public AudioClip clipShield;
    public AudioClip clipEndurance;


    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {      
        if(other.tag == "Player")
        {
            if (gameObject.tag == "PotionHealth")
            {
                BackPackScript.HealthPotionAmount++;
                audioSource.PlayOneShot(clipHealth);
                Destroy(gameObject, 0.3f);
            }
            if(gameObject.tag == "PotionShield")
            {
                BackPackScript.ShieldPotionAmount++;
                audioSource.PlayOneShot(clipShield);
                Destroy(gameObject, 0.3f);
            }
        
            if (gameObject.tag == "PotionEndurance")
            {
                BackPackScript.EndurancePotionAmount++;
                audioSource.PlayOneShot(clipEndurance);
                Destroy(gameObject, 0.3f);
            }
        }
    }
    
}
