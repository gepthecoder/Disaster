using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public WheelCollider WheelFL;
    public WheelCollider WheelFR;
    public WheelCollider WheelBL;
    public WheelCollider WheelBR;

    public GameObject FL;
    public GameObject FR;
    public GameObject BL;
    public GameObject BR;

    public float topSpeed = 250f;
    public float maxTorque = 2000f; //the max torque to apply to wheels
    public float maxSteerAngle = 45f;
    public float currentSpeed;
    public float maxBrakeTorque = 2200;

    public float Forward; //forward axis
    public float Turn; //turn axis
    public float Brake; //brake axis

 //   private Rigidbody rb; //rigid body of car


 //   void Start () {

 //       rb = GetComponent<Rigidbody>();
	//}
	
	void FixedUpdate () {

        Forward = Input.GetAxis("Vetical");
        Turn = Input.GetAxis("Horizontal");
        Brake = Input.GetAxis("Jump");

        WheelFL.steerAngle = maxSteerAngle * Turn;
        WheelFR.steerAngle = maxSteerAngle * Turn;

        currentSpeed = 2 * 22 / 7 * WheelBL.radius * WheelBL.rpm * 60 / 1000; //formula for calculating speed in kmph

        if(currentSpeed < topSpeed)
        {
            WheelBL.motorTorque = maxTorque * Forward; //run the wheels on back lef and back right
            WheelBL.motorTorque = maxTorque * Forward;
        }

        WheelBL.brakeTorque = maxBrakeTorque * Brake;
        WheelBR.brakeTorque = maxBrakeTorque * Brake;
        WheelFL.brakeTorque = maxBrakeTorque * Brake;
        WheelFR.brakeTorque = maxBrakeTorque * Brake;
    }

    void Update() //once per frame
    {
        Quaternion flq; //rotation of wheel collider
        Vector3 flv; //position of wheel collider
        WheelFL.GetWorldPose(out flv, out flq); //get wheel collider position and rotation
        FL.transform.position = flv;
        FL.transform.rotation = flq;

        Quaternion Blq; //rotation of wheel collider
        Vector3 Blv; //position of wheel collider
        WheelBL.GetWorldPose(out Blv, out Blq); //get wheel collider position and rotation
        BL.transform.position = Blv;
        BL.transform.rotation = Blq;

        Quaternion fRq; //rotation of wheel collider
        Vector3 fRv; //position of wheel collider
        WheelFR.GetWorldPose(out fRv, out fRq); //get wheel collider position and rotation
        FR.transform.position = fRv;
        FR.transform.rotation = fRq;

        Quaternion BRq; //rotation of wheel collider
        Vector3 BRv; //position of wheel collider
        WheelBR.GetWorldPose(out BRv, out BRq); //get wheel collider position and rotation
        BR.transform.position = BRv;
        BR.transform.rotation = BRq;
    }
}
