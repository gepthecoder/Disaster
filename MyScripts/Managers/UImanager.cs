using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public static bool ExitVehiclePressed;
    public static bool EnterVehiclePressed;
    public static bool inRangeToEnterCar;
    public static bool usingThirdPersonControls;

    public GameObject GUI_PlayerControls;
    public GameObject GUI_VehicleControls;

    public GameObject GUI_EnterVehicle;

    public GameObject GUI_ExitVehicle;

    public GameObject GUI_ReloadWeapon;

    public bool rewardedVideoOpened;
    public bool buyNoAdsOpened;

    public GameObject noAdsUI;
    public GameObject rewardedVideoUI;

    void Start()
    {
        usingThirdPersonControls = true;
        inRangeToEnterCar = false;
        ExitVehiclePressed = false;
        EnterVehiclePressed = false;

        rewardedVideoOpened = false;
        buyNoAdsOpened = false;
    }

    void Update()
    {
        // Handle control view
        ManageControls();
        // Handles Graphics for Get In Car Button
        ManageGetInCar();
        // Handle rewardedVideo UI & NoAdsUI
        ManageUIboolValues();

        //show GUI for reloading when noBulletsInMags
        if(AmmoManager.CurrentBulletsInMag <= 0 || AmmoManager.CurrentExplosiveBulletsInMag <= 0)
        {
            //we have no bullets in magazin
            //show GUI -> Reload
            GUI_ReloadWeapon.SetActive(true);
        }
        else
        {
            //we have bullets in magazin
            //hide GUI -> Reload
            GUI_ReloadWeapon.SetActive(false);
        }
      
    }

    private void ManageControls()
    {
        if (usingThirdPersonControls)
        {
            //show player controls
            GUI_PlayerControls.SetActive(true);
            GUI_VehicleControls.SetActive(false);
        }
        else
        {
            //show vehicle controls
            GUI_VehicleControls.SetActive(true);
            GUI_PlayerControls.SetActive(false);
        }
    }

    private void ManageGetInCar()
    {
        if (inRangeToEnterCar)
        {
            GUI_EnterVehicle.SetActive(true);
        }
        else { GUI_EnterVehicle.SetActive(false); }
    }

    public void ExitVehicle_OnClickEvent()
    {
        ExitVehiclePressed = true;
    }

    public void EnterVehicle_OnClickEvents()
    {
        EnterVehiclePressed = true;
    }


    public void ManageUIboolValues()
    {
        if(noAdsUI.activeSelf == true)
        {
            buyNoAdsOpened = true;
            Time.timeScale = 0;
        }else if(rewardedVideoUI.activeSelf == true)
        {
            rewardedVideoOpened = true;
            Time.timeScale = 0;

        }
        else
        {
            buyNoAdsOpened = false;
            rewardedVideoOpened = false;
            Time.timeScale = 1;
        }
    }

}
