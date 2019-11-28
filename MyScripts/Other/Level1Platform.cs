using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Platform : MonoBehaviour
{

    public static bool isOnLevel1Platform;

    public GameObject TryOne;
    public AudioClip audioClip;

    //For Bridge Platform textMesh Components
    public static int overAllKills;
    public static int overAllBossKills;
    public static string WaveName;

    public GameObject overAllKillsTxtMesh;
    public GameObject overAllWaveTxtMesh;
    public GameObject overAllBossKillsTxtMesh;

    public static int remainingKills = 150;
    public static int remainingBossKills = 15;

    public GameObject remainingKillsTxtMesh;
    public GameObject remainingBossKillsTxtMesh;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void DisplayOverallValues() {
        overAllKillsTxtMesh.GetComponent<TextMesh>().text = "KILLS: " + overAllKills.ToString();
        overAllBossKillsTxtMesh.GetComponent<TextMesh>().text = "BOSS KILLS: " + overAllBossKills.ToString();
        overAllWaveTxtMesh.GetComponent<TextMesh>().text = "WAVE : " + WaveName;
    }
    public void DisplayRemainingValues() {
        remainingKillsTxtMesh.GetComponent<TextMesh>().text = "KILLS REMAINING: " + remainingKills.ToString();
        remainingBossKillsTxtMesh.GetComponent<TextMesh>().text = "BOSS KILLS REMAINING: " + remainingBossKills.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isOnLevel1Platform = true;
            //Display UI Stats or some sort of text mesh in front of player ?? 
            TryOne.SetActive(true);

            DisplayOverallValues();
            DisplayRemainingValues();

            audioSource.PlayOneShot(audioClip);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Display UI Stats or some sort of text mesh in front of player ?? 
            TryOne.SetActive(false);
            isOnLevel1Platform = false;
        }
    }


}
