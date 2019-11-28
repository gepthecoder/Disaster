using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public static bool CanAttack = false;

   void OnTriggerEnter(Collider other) {

       if(other.gameObject.tag == "Player") {
            CanAttack = true;
        }    
        
    }

    void OnTriggerExit(Collider other) {

        if (other.gameObject.tag == "Player") {
            CanAttack = false; 
        }
    }
}
