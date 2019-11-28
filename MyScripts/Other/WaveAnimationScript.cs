using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveAnimationScript : MonoBehaviour
{
    public GameObject Wave01TextFX;
    public static bool SetAnimation_Wave01;
    public static string WaveName;

    public AudioClip audioClip;
    private AudioSource audioSource;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayAnimation_Wave01() {

        Animator animator = Wave01TextFX.GetComponent<Animator>();
        if(animator != null) {
            audioSource.PlayOneShot(audioClip);
            SetWaveText(WaveName);
            animator.SetTrigger("Wave01");      
        }
    }

    private void SetWaveText(string waveName) {
        Wave01TextFX.GetComponent<Text>().text = waveName;
    }

    void Update() {

        if (SetAnimation_Wave01) {
            PlayAnimation_Wave01();
            SetAnimation_Wave01 = false;
        }   
    }

}
