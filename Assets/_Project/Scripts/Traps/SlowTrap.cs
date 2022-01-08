using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class SlowTrap : AOETrap
{
    public Slow slow;
    private float m_dur;

    List<Enemy> enemies = new List<Enemy>();
    private void Start()
    {
        m_dur = slow.slowDuration;
       
        
    }
    private void Update()
    {
        if (!isDragging)
        { 
            if(m_dur == slow.slowDuration){
                AudioManager.instance.Play("Mud");
            }
            m_dur -= Time.deltaTime;
        }
        if(m_dur <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Enemy") && !isDragging)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.DecreaseSpeed(slow.slowSpeed);
            enemies.Add(enemy);
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && !isDragging)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.ResetSpeed();
        }
    }

    private void OnDestroy()
    {
        if (enemies.Count == 0 || enemies == null) return;
        if(enemies != null)
        {
            if (enemies.Count > 0)
            {
                foreach (Enemy enemy in enemies)
                {
                    if(enemy != null)
                    enemy.GetComponent<Enemy>().ResetSpeed();

                }
            }
        }

    }
}
