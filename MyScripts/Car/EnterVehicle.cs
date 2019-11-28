using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TO:DO --> Remote spawning vehicle from backpack


public class EnterVehicle : MonoBehaviour {

    public GameObject Vehicle;
    public GameObject playerBackUp;

    private GameObject Player;
    
    //public AudioClip audioS;
    //public Camera cam;

    public static bool inVehicle = false;

    VehicleBehaviour.WheelVehicle wheelVehicle;
    VehicleBehaviour.EngineSoundManager engineSoundManager;
    VehicleBehaviour.Utils.CameraFollow cam;

    //GameObject guiObj; //for pressin 'e' to enter vehicle
    GameObject guiObjCrossHair;
 
    //AudioSource source;
    
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        guiObjCrossHair = GameObject.Find("Crosshair");

    }

    void Start () {              

        wheelVehicle = GetComponent<VehicleBehaviour.WheelVehicle>();
        wheelVehicle.enabled = false;

        engineSoundManager = GetComponent<VehicleBehaviour.EngineSoundManager>();
        engineSoundManager.enabled = false;

        //cam = GetComponent<VehicleBehaviour.Utils.CameraFollow>();
        //cam.enabled = false;

        //source = GetComponent<AudioSource>();

        //cam.enabled = false;
        playerBackUp.SetActive(false);
	}

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player" && inVehicle == false)
        {
            //EnterVehicleUI.SetActive(true);
            Debug.Log("Show UI!");
            //EnterVehicleUI.GetComponent<Animator>().SetTrigger("showGetINCar");
            UImanager.inRangeToEnterCar = true;
        }

        if (other.gameObject.tag == "Player" && inVehicle == false && UImanager.EnterVehiclePressed)
        {
            Debug.Log("Hide UI!");
            //source.PlayOneShot(audioS, 1);
            //EnterVehicleUI.SetActive(false);
            //EnterVehicleUI.GetComponent<Animator>().SetTrigger("closeShowGetINCar");
            UImanager.inRangeToEnterCar = false;
            UImanager.usingThirdPersonControls = false;
            UImanager.EnterVehiclePressed = false;

            guiObjCrossHair.SetActive(false);
            playerBackUp.SetActive(true);
            Player.SetActive(false);
            Player.transform.parent = Vehicle.transform;
            wheelVehicle.enabled = true;
            engineSoundManager.enabled = true;
            inVehicle = true;
            //cam.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //EnterVehicleUI.SetActive(false);
            //EnterVehicleUI.GetComponent<Animator>().SetTrigger("closeShowGetINCar");
            UImanager.inRangeToEnterCar = false;
            Debug.Log("Hide UI! 11");

        }
    }
	
	// Update is called once per frame
	void Update () {

        //exit vehicle
        if(inVehicle == true && UImanager.ExitVehiclePressed) 
        {
            playerBackUp.SetActive(false);
            Player.SetActive(true);
            Player.transform.parent = null;
            Player.transform.rotation = Quaternion.Euler(0, 0, 0);
            wheelVehicle.enabled = false;
            engineSoundManager.enabled = false;
            inVehicle = false;
            //cam.enabled = false;
            guiObjCrossHair.SetActive(true);

            UImanager.usingThirdPersonControls = true;
            UImanager.ExitVehiclePressed = false;         

        }


    }

    public static bool playerInVehicle()
    {
        return inVehicle;
    }

}
