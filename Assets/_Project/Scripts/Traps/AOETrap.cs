using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOETrap : MonoBehaviour
{
    protected Collider[] enemyList(int AOE)
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, AOE, 1 << 7);
        return enemies;
    }

    protected bool isEnemyDetected(int AOE)
    {

        if (enemyList(AOE) != null)
        {
            return true;
        }
        return false;
    }
}