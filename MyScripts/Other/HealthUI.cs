using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    [SerializeField]
    private Text healthUI;

    private PlayerHealth health;

    private int currentHealth;

    private void Awake()
    {
        healthUI = GetComponent<Text>();
        health = GetComponent<PlayerHealth>();
    }
	
	// Update is called once per frame
	void Update () {

        currentHealth = health.currentHealth;
        healthUI.text = "Health: " + currentHealth;
    }
}
