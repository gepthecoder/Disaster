using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeThrow : MonoBehaviour
{
    public static bool GranadeHasBeenThrown = false;

    public float throwForce = 70f;

    public GameObject granadePrefab;
    private Animator anime;

    public GameObject player;
    public AudioClip clipOfGrandePull;
    private AudioSource audioSource;

    
    void Awake()
    {
        anime = GetComponentInParent<Animator>();
        audioSource = player.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            //play sound of granade trigger
            audioSource.PlayOneShot(clipOfGrandePull);
            
            anime.SetTrigger("CanThrowGranade");
            StartCoroutine(ConfigureGranadeThrow());
            GranadeHasBeenThrown = true;
        }
    }

    private void ThrowGranade()
    {
        GameObject granade = Instantiate(granadePrefab, transform.position, transform.rotation);
        Rigidbody rb = granade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        
    }

    IEnumerator ConfigureGranadeThrow()
    { 
        yield return new WaitForSeconds(2f);
        ThrowGranade(); 
    }
}
