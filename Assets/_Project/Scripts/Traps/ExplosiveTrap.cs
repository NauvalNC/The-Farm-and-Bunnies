using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTrap : AOETrap
{
    public Explosive explosiveStats;

    void Update()
    {
        if (isEnemyDetected(explosiveStats.AOE))
        {
            //play animation
        }
    }
    
    //dispatch when animation
    public void Explode()
    {
        foreach(Collider enemy in enemyList(explosiveStats.AOE))
        {
            enemy.GetComponent<Enemy>().OnDamage(explosiveStats.DMG);
        }
    }

    //dispatch after animation finished
    public void destroyObject()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosiveStats.AOE);
    }
}
