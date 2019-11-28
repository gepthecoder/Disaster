using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private float sensitivity = 0.05f;

    private CinemachineComposer composer;

    void Start () {

        composer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
	}
	
	void Update () {

        //vertical mouse movement
        float vertical = Input.GetAxis("Mouse Y") * sensitivity;
        composer.m_TrackedObjectOffset.y += vertical;
        composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, 2f, 5.7f);
    
    }

   
}
