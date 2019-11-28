using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmmiter : MonoBehaviour {

    [SerializeField]
    private GameObject firePoint;

    [SerializeField]
    private GameObject Bullet;

    [SerializeField]
    private float BulletForce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire1")){

            //creating a bullet instance
            GameObject temp_bullet;
            temp_bullet = Instantiate(Bullet, firePoint.transform.position, firePoint.transform.rotation) as GameObject;

            //temp_bullet.transform.Rotate(Vector3.left * 90);

            //instantiating bullet
            Rigidbody temp_RigidBody;
            temp_RigidBody = temp_bullet.GetComponent<Rigidbody>();

            //tell the bullet to be "pushed" forward
            temp_RigidBody.AddForce(transform.forward * BulletForce);

            //destroj bullets after 10sec
            Destroy(temp_bullet, 10.0f);
            
        }
		
	}
}
