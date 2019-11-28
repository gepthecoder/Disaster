using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Timeline;

public class ShopTriggerPlatformScript : MonoBehaviour
{
    public static bool SteppedOnPlatform = false;

    public GameObject Player;
    //private myThirdPersonController gun;
    //Shop UI
    public GameObject ShopUI;
    //public GameObject PressP;

    //Audio
    public AudioClip entrySound;
    private AudioSource audioSource;

    public FixedButton CloseShopButton;

    //int helpInt;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //gun = Player.GetComponent<myThirdPersonController>();
        ////helpInt = 0;
    }

    void Update()
    {

        //Debug.Log("Help int!! " + helpInt);

        //if (CloseShopButton.Pressed)
        //{
        //    if(helpInt == 0)
        //    {
        //        CloseShop();
        //        helpInt++;
        //    }
        //}

    }


    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject == Player)
        {
            Debug.Log("Enter trigger " + SteppedOnPlatform);
            //Play sound
            audioSource.PlayOneShot(entrySound);

            SteppedOnPlatform = true; //for animation of seller

            OpenShopUI();
        }
    }

    void OnTriggerExit(Collider other)
    {
        SteppedOnPlatform = false;
        //helpInt = 0;
        Debug.Log("Exiiit Trigger " + SteppedOnPlatform);
    }


    public void OpenShopUI()
    {    
        ShopUI.SetActive(true);
        Debug.Log(ShopUI.activeSelf + " ShopUI ");

    }

    public void CloseShop()
    {
        //ShopButton_Anime.SetTrigger("hideShop");
        ShopUI.SetActive(false);
        Debug.Log(ShopUI.activeSelf + " ShopUI ");

    }


}
