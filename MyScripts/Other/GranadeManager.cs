using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeManager : MonoBehaviour
{
    public AudioClip boomSound;

    private AudioSource audioSource;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        if (GranadeScript.hasExploded)
        {
            audioSource.PlayOneShot(boomSound);
            GranadeScript.hasExploded = false;
        }
    }
}
