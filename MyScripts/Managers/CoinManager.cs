using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CoinManager : MonoBehaviour
{
    public static bool PickedUp = false;

    public Text coinCountText;
    public AudioClip coinPickUpSound;

    public static int numOfCoins = 20;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (PickedUp)
        {
            audioSource.PlayOneShot(coinPickUpSound);
            PickedUp = false;
        }

        coinCountText.text = numOfCoins.ToString();
        
    }
}
