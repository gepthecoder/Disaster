using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//TO:DO --> PotionShield
        //Enemies first damage shield and then health

//TO:DO --> PotionEndurance
        //Player Jump & Speed become increased

public class BackPackScript : MonoBehaviour
{
    public static bool BackPackIsOpen = false;
    public static bool HealthPotionIsUsed = false;
    public static bool EnduranceIncreaseBool = false;

    public static int GranateAmount;
    public static int HealthPotionAmount;
    public static int ShieldPotionAmount;
    public static int EndurancePotionAmount;

    public GameObject player;
    private Gun gun;

    public GameObject BackPackUI;

    public AudioClip audioClip;

    public AudioClip audioClip1;

    //Set UI elements of items
    public Text GranateAmountText;
    public Text HealthPotionText;
    public Text ShieldPotionText;
    public Text EndurancePotionText;

    //slider for Health
    public Slider healthSlider;

    //slider for Shield
    public Slider ShieldSlider;

    //slider for Endurance
    public Slider enduranceSlider;

    private AudioSource audioSource;

    public FixedButton backPackBtn;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        BackPackUI.SetActive(false);

        //gun = player.GetComponentInChildren<Gun>();
    }

    void Update()
    {
        SetUIvalues();

        if (backPackBtn.Pressed)
        {
            OpenOrCloseBackback();
        }
        
    }

    public void OpenOrCloseBackback()
    {
        if (BackPackIsOpen)
        {
            //close
            BackPackUI.SetActive(false);
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            BackPackIsOpen = false;
            //gun.enabled = true;
        }
        else
        {
            //open
            BackPackUI.SetActive(true);

            audioSource.PlayOneShot(audioClip); //play sound
                                                //Cursor.lockState = CursorLockMode.None;
                                                //Cursor.visible = true;
            BackPackIsOpen = true;
            //gun.enabled = false;
        }
    }

    void SetUIvalues()
    {
        GranateAmountText.text = GranateAmount.ToString();

        HealthPotionText.text = HealthPotionAmount.ToString();

        ShieldPotionText.text = ShieldPotionAmount.ToString();

        EndurancePotionText.text = EndurancePotionAmount.ToString();
    }

    public void UseHealthPotion()
    {
        if(HealthPotionAmount > 0)
        {
            HealthPotionAmount--;
            HealthPotionIsUsed = true;

            //Change Slider value
            healthSlider.value += 15;
            audioSource.PlayOneShot(audioClip1);
            //TO:DO --> play effect for drinking health potion
        }
    }

    public void UseShieldPotion()
    {
        if (ShieldPotionAmount > 0)
        {
            ShieldPotionAmount--;
            ShieldSlider.value += 2; 
            audioSource.PlayOneShot(audioClip1);

            //TO:DO --> play effect for drinking health potion
        }
    }

    public void UseEndurancePotion()
    {
        if (EndurancePotionAmount > 0)
        {
            EndurancePotionAmount--;
            EnduranceIncreaseBool = true;
            //TO:DO --> for 20sec speed, jump & points are doubled
            audioSource.PlayOneShot(audioClip1);
            //TO:DO --> play effect for drinking endurance potion
        }
    }

}
