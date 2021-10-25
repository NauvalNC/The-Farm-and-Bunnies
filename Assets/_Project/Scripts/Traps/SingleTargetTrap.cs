using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class SingleTargetTrap : Trap
{
    public GameObject target;

    protected bool isEnemyDetected()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,transform.up,out hit, 1 << 7 ) && target == null){
            print(hit.transform.name);
            target = hit.transform.gameObject;
            
        }

        if (target == null)
        {
            return false;
        }
            
        else 
        {
            return true;
        }
    } 
}
