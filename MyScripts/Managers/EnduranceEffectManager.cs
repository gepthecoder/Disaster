using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnduranceEffectManager : MonoBehaviour
{

    public static bool PotionActivated;
    public static bool KillConfirmed;

    public GameObject enduranceImage;
    private Animator enduranceImageAnime;

    public Slider enduranceSlider;
    private Animator enduranceSliderAnime;
    

    private int startValue = 0;
    public int currentValue;

    private int ComboAttackValue = 30;

    public AudioClip boostSound;
  

    private AudioSource audioSource;

    private float timer = 15.0f;

    void Awake()
    {
        currentValue = startValue;
        enduranceSlider.value = currentValue;

        enduranceSliderAnime = enduranceSlider.GetComponent<Animator>();
        enduranceImageAnime = enduranceImage.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (KillConfirmed)
        {          
            currentValue += 2;
            enduranceSlider.value = currentValue;
            KillConfirmed = false;
        }

        if(currentValue >= ComboAttackValue)
        {
            //Play animation for endurance slider
            currentValue = 30;
            PlayAnimationForSlider();
            if (Input.GetKey(KeyCode.G))
            {
                PlayAnimationForImage();
                PotionActivated = true;
                //Play sound
                audioSource.PlayOneShot(boostSound);
                StartCoroutine("EnduranceEffect");

            }
        }

        if (BackPackScript.EnduranceIncreaseBool)
        {

            BackPackScript.EnduranceIncreaseBool = false;
            currentValue += 10;
            enduranceSlider.value = currentValue;
        }
    }

     IEnumerator EnduranceEffect()
    {
        yield return new WaitForSeconds(2f);
        currentValue -= 1;
        enduranceSlider.value = currentValue;

        if(currentValue > 0)
        {
            StartCoroutine("EnduranceEffect");
        }
        else { EnduranceAfterEffect(); }     
    }

    private void EnduranceAfterEffect()
    {
        PotionActivated = false;
        //set back to normal
        enduranceSlider.value = 0;
        StopAnimationForSlider();
        StopAnimationForImage();
    }

    private void PlayAnimationForSlider()
    {
        enduranceSliderAnime.SetBool("EnduranceEffectSlider", true);
    }

    private void StopAnimationForSlider()
    {
        enduranceSliderAnime.SetBool("EnduranceEffectSlider", false);
    }

    private void PlayAnimationForImage()
    {
        enduranceImageAnime.SetBool("enduranceImage", true);
    }

    private void StopAnimationForImage()
    {
        enduranceImageAnime.SetBool("enduranceImage", false);
    }

}
