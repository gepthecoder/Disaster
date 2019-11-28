using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public AudioClip enemyCloseBy;


    private AudioSource source;
    private EnemyMovement enemy;


    private void Awake()
    {
        source = GetComponent<AudioSource>();
        enemy = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        float remainingDistance = enemy.RemainingDistance();

        if(remainingDistance <= 40f)
        {
            PlaySound1();
            
        }
    }

    private void PlaySoundEnemyCloseBy()
    {
        source.PlayOneShot(enemyCloseBy);
    }

    IEnumerator PlaySound1()
    {
        PlaySoundEnemyCloseBy();
        yield return new WaitForSeconds(10);
    }
}
