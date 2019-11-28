using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour
{
    public FixedButton ReloadButton;
    public GameObject player;

    public Text AmountOfAmmoTxT;
    public Text AmountOfExplosiveAmmoText;

    public GameObject WeaponAmmoUI;
    public GameObject WeaponExplosiveAmmoUI;

    public void SetValuesForBackPack(int normalRounds, int explosiveRounds)
    {
        AmountOfAmmoTxT.text = normalRounds.ToString();
        AmountOfExplosiveAmmoText.text = explosiveRounds.ToString();
    }

    public int NumberOfBulletsTotal(int currentRoundsInMag, int currentAmmoCount)
    {
        return currentRoundsInMag + currentAmmoCount;
    }

    public int NumberOfExplosiveBulletsTotal(int currentExRoundsInMag, int currentExAmmoCount)
    {
        return currentExRoundsInMag + currentExAmmoCount;
    }

    public Toggle bulletToggel;

    public static bool bulletToggelBool;

    //Ordinary ammo
    public static int CurrentBulletsInMag;
    public static int CurrentAmmoCount;

    public static bool HasBulletsInMagazin = true;

    public GameObject bulletsInMagazin;
    private Text txtBulletsInMagazin;

    public GameObject allAmmo;
    private Text txtAllAmmo;

    private int maxBulletsInMagazin = 30;
    private int maxAmmo = 999; 
    public int currentBullets;
    private int startAmmo = 120;

    //Explosive ammo
    public static int CurrentExplosiveBulletsInMag;
    public static int CurrentExplosiveAmmoCount;

    public static bool HasExplosiveBulletsInMagazin = true;

    public GameObject ExplosivebulletsInMagazin;
    private Text txtExplosiveBulletsInMagazin;

    public GameObject allExplosiveAmmo;
    private Text txtAllExplosiveAmmo;

    private int maxExplosiveBulletsInMagazin = 5;
    private int maxExplosiveAmmo = 999;
    public int currentExplosiveBullets;
    private int startExplosiveAmmo = 10;

    public AudioClip reloadAmmoClip;
    private AudioSource audioSource;

    void Awake()
    {

        bulletToggelBool = bulletToggel.isOn;
        WeaponAmmoUI.SetActive(true);
        WeaponExplosiveAmmoUI.SetActive(false);

        txtBulletsInMagazin = bulletsInMagazin.GetComponent<Text>();
        txtAllAmmo = allAmmo.GetComponent<Text>();

        txtExplosiveBulletsInMagazin = ExplosivebulletsInMagazin.GetComponent<Text>();
        txtAllExplosiveAmmo = allExplosiveAmmo.GetComponent<Text>();

        CurrentBulletsInMag = maxBulletsInMagazin;
        CurrentAmmoCount = startAmmo;

        CurrentExplosiveBulletsInMag = maxExplosiveBulletsInMagazin;
        CurrentExplosiveAmmoCount = startExplosiveAmmo;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        SetValuesForBackPack(NumberOfBulletsTotal(CurrentBulletsInMag, CurrentAmmoCount), NumberOfExplosiveBulletsTotal(CurrentExplosiveBulletsInMag, CurrentExplosiveAmmoCount));

        bulletToggelBool = bulletToggel.isOn;

        if (bulletToggelBool)
        {
            WeaponExplosiveAmmoUI.SetActive(false);
            WeaponAmmoUI.SetActive(true);

            currentBullets = CurrentBulletsInMag;

            txtBulletsInMagazin.text = CurrentBulletsInMag.ToString();
            txtAllAmmo.text = CurrentAmmoCount.ToString();

            //the magazin has no bullets left
            if (CurrentBulletsInMag <= 0)
            {
                HasBulletsInMagazin = false;

            }

            if (!HasBulletsInMagazin && CurrentAmmoCount > 0)
            {
                //reloadWeapon
                if (ReloadButton.Pressed)
                {
                    StartCoroutine("ReloadWeapon");
                    CurrentBulletsInMag += 30;
                    CurrentAmmoCount -= 30;
                    HasBulletsInMagazin = true;
                }
            }

            if (CurrentBulletsInMag > 30)
            {
                CurrentBulletsInMag = 30;
            }

            if (CurrentAmmoCount > 999)
            {
                CurrentAmmoCount = 999;
            }
        }
        else
        {
            WeaponAmmoUI.SetActive(false);
            WeaponExplosiveAmmoUI.SetActive(true);

            currentExplosiveBullets = CurrentExplosiveBulletsInMag;

            txtExplosiveBulletsInMagazin.text = CurrentExplosiveBulletsInMag.ToString();
            txtAllExplosiveAmmo.text = CurrentExplosiveAmmoCount.ToString();

            //the magazin has no explosive bullets left
            if (CurrentExplosiveBulletsInMag <= 0)
            {
                HasExplosiveBulletsInMagazin = false;

            }

            if (!HasExplosiveBulletsInMagazin && CurrentExplosiveAmmoCount > 0)
            {
                //reloadWeapon
                if (ReloadButton.Pressed)
                {
                    StartCoroutine("ReloadExplosiveWeapon");
                    CurrentExplosiveBulletsInMag += 5;
                    CurrentExplosiveAmmoCount -= 5;
                    HasExplosiveBulletsInMagazin = true;
                }
            }

            if (CurrentExplosiveBulletsInMag > 5)
            {
                CurrentBulletsInMag = 5;
            }

            if (CurrentExplosiveAmmoCount > 999)
            {
                CurrentExplosiveAmmoCount = 999;
            }
        }
       
     
    }

   IEnumerator ReloadWeapon()
    {
        myThirdPersonController.isReloading = true;
        audioSource.PlayOneShot(reloadAmmoClip);
        yield return new WaitForSeconds(2f);
        myThirdPersonController.isReloading = false;
    }

    IEnumerator ReloadExplosiveWeapon()
    {
        myThirdPersonController.isReloading = true;
        audioSource.PlayOneShot(reloadAmmoClip);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(reloadAmmoClip);
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(reloadAmmoClip);
        myThirdPersonController.isReloading = false;
    }

}
