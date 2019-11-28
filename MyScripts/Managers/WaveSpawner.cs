using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public AudioClip waveCompletedAudio;
    public GameObject AnimationWaveParent;

    public static int enemiesLeft;
    public static int BeastsLeft;
    public static int scorePoints = 5000;

    public static int wavesCount;
    public static bool waveStartBool;
    public string currentWaveName;

    public enum SpawnState { SPAWNING, WAITING, COUNTING }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    public float bossTimer;
    public bool timeStarted = false;


    //BOSS SET-UP
    public Transform[] BossSpawnPoints;
    public GameObject[] Bosses;


    //Wave Sound Manager --> for Boss
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    //UI GO
    public Text bossTimeTxt;
    public Text enemiesLeftTxt;
    public Text scorePointsTxt;
    public Text beastsLeftTxt;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        waveCountdown = timeBetweenWaves;

        bossTimer = 70;
        timeStarted = true;

        AnimationWaveParent.SetActive(false);       
    }

    void Update()
    {
        enemiesLeftTxt.text = enemiesLeft.ToString();
        scorePointsTxt.text = scorePoints.ToString();
        beastsLeftTxt.text = BeastsLeft.ToString();

        if(timeStarted == true)
        {
            bossTimer -= Time.deltaTime;
            DisplayBossTimeOnUI(bossTimer);

            if(bossTimer <= 0)
            {
                bossTimer = 70;

                GameObject go = Bosses[Random.Range(0, Bosses.Length)];
                Transform bp = BossSpawnPoints[Random.Range(0, BossSpawnPoints.Length)];
                AudioClip ac = audioClips[Random.Range(0, audioClips.Length)];

                Instantiate(go, bp.position, bp.rotation);

                audioSource.PlayOneShot(ac);
                BeastsLeft++;
            }
        }

        if(state == SpawnState.WAITING)
        {
            //Check if enemies are still alive
            if (!EnemyIsAlive())
            {              
                //Begin new round
                WaveCompleted();
            }
            else { return; }
        }

        if(waveCountdown <= 0) //if its time to spawn waves
        {

            if (state != SpawnState.SPAWNING)
            {
                Debug.Log(WaveAnimationScript.SetAnimation_Wave01);
                //Play Animation For Wave
                AnimationWaveParent.SetActive(true);
                WaveAnimationScript.SetAnimation_Wave01 = true;

                Debug.Log(WaveAnimationScript.SetAnimation_Wave01);
                waveStartBool = true;
                StartCoroutine(SpawnWave(waves[nextWave]));
            } 
        }
        else
        {
            waveCountdown -= Time.deltaTime; //for real time proccessing
        }

        if (Level1Platform.isOnLevel1Platform)
        {    

        }
    }

    void DisplayBossTimeOnUI(float timer)
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);

        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        bossTimeTxt.text = niceTime;
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed!");
        audioSource.PlayOneShot(waveCompletedAudio);
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves complete!! Looping...");
        }
        else { nextWave++; }    
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                //if we dont find an enemy, there are no enemies alive
                return false;
            }
        }     
        return true;
    }

    public IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave: " + _wave.name);
        currentWaveName = _wave.name;
        //Set name of wave animation
        WaveAnimationScript.WaveName = currentWaveName;
        Level1Platform.WaveName = currentWaveName; 
        state = SpawnState.SPAWNING; //now we are actually spawning       
        //Spawn
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //Spawn enemy
        Debug.Log("Spawning enemy: " + _enemy.name );

        if(spawnPoints.Length == 0) { Debug.LogError("No spawn points in project!"); }

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);

        enemiesLeft++;  
    }


}
