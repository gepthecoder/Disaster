using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

    public WaveSpawner waveSpawner;

    public Transform respawnPosition;

    private GameObject currentPlayer;

    public GameObject spawnEffect_FX;

    public PlayerHealth playerHealth;
    public float restartDelay = 5f;

    public Text statsText;

    Animator anim;
    float restartTimer;


    void Awake() {
        anim = GetComponent<Animator>();
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        if (playerHealth.isDead) {
            StartCoroutine(ExecuteCall());
        }
        else { anim.SetBool("GameOver", false); }
    }

    IEnumerator ExecuteCall()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("GameOver", true);
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnPlayer_WatchedAd());
    }

    public IEnumerator RespawnPlayer_WatchedAd()
    {
        // hide gameover UI
        anim.SetTrigger("hideGameOver");
        // add 20 health to new player TO:DO extract health to zero add 20
        PlayerHealth.playerWatchedAD_extraChance = true;
        // move player position
        currentPlayer.transform.position = respawnPosition.position;
        currentPlayer.transform.rotation = respawnPosition.rotation;
        // show effect of respawn --> shoot&choose asset
        GameObject go = spawnEffect_FX;
        go.transform.localScale = new Vector3(3, 3, 3);
        Instantiate(go, respawnPosition.position, respawnPosition.rotation);
        Destroy(go, 4f);

        //restart current reached wave
        yield return new WaitForSeconds(3f);
        currentPlayer.GetComponent<myThirdPersonController>().enabled = true;
        currentPlayer.GetComponent<Animator>().SetTrigger("backToLive");

        Debug.Log(lastWaveNumber() + " Last wave number");
        StartCoroutine(waveSpawner.SpawnWave(waveSpawner.waves[lastWaveNumber()]));
        //StartCoroutine(WaitAndSetAnimeBackToNormal());

        yield break;
    }

    public string lastWaveName()
    {
        return waveSpawner.currentWaveName;
    }

    int lastNum;
    private int lastWaveNumber()
    {
        for (int i = 1; i <= 10; i++)
        {
            if(lastWaveName() == "WAVE " + i)
            {
                lastNum = i;               
            }
        }

        return lastNum;
    }

    public void RestartGame()
    {

        SceneManager.LoadScene(0);
    }

    




}
