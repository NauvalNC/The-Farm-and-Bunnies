using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTrap : AOETrap
{
    public Explosive explosiveStats;
    private float m_explodeDur;
    bool m_trigger, m_hasDestroy;
    private void Start()
    {
        m_explodeDur = explosiveStats.explodeDur; 
    }
    void Update()
    {
        if (m_trigger) 
        { 
            m_explodeDur -= Time.deltaTime; 
            transform.GetChild(0).gameObject.SetActive(true); 
        }

        if(m_explodeDur <= -.1f)
        {
            Explode();
            AudioManager.instance.Play("Bomb");
            Destroy(gameObject);

        }
        
    }
    
    public void Explode()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        GameObject FX = Instantiate(transform.GetChild(1).gameObject, transform.position, Quaternion.identity);
        FX.SetActive(true);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isDragging)
        {
            m_trigger = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && m_explodeDur <= 0)
        {
            other.GetComponent<Enemy>().OnDamage(explosiveStats.DMG);
                       
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosiveStats.AOE);
    }
}
