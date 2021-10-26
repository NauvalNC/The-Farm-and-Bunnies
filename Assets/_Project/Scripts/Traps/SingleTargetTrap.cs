using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class SingleTargetTrap : Trap
{
    public GameObject target;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy") && target == null)
        {
            target = other.gameObject;
        }
    }
}
