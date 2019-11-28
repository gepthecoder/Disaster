using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EnemyExtension {

    public static bool EnemyAttackBool() { return EnemyTrigger.CanAttack;   }

    public static GameObject EnemyAttackGO(GameObject go) 
    {
        return go; 
    }


}
    
