using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTrap : MonoBehaviour
{
    public Explosive explosiveStats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemyDetected())
        {
            //play animation
        }
    }
    Collider[] enemyList()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosiveStats.AOE, 1 << 7);
        return enemies;
    }

    bool isEnemyDetected()
    {
        
        if (enemyList() != null)
        {
            return true;
        }
        return false;
    }

    //dispatch after animation
    public void Explode()
    {
        foreach(Collider enemy in enemyList())
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
