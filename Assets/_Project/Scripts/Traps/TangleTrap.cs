using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TangleTrap : SingleTargetTrap
{
    public Tangle tangleStats;
    float m_tangleDur;
    Animator m_animator;
    bool m_trigger;
    private void Start()
    {
        m_tangleDur = tangleStats.tangleDuration;
        m_animator = GetComponent<Animator>();
        
    }
    // Update is called once per frame
    void Update()
    {
        
        if (m_trigger)
        {
            m_tangleDur -= Time.deltaTime;
            if (m_tangleDur > 0)
            {
                target.GetComponent<Enemy>().Tangle();
                m_animator.SetTrigger("Catch");
                
                if(target.gameObject.activeInHierarchy == true)
                {
                    AudioManager.instance.Play("Trap");
                }
               target.gameObject.SetActive(false);

            }
            else
            {
                
                target.GetComponent<Enemy>().ResetSpeed();
                target.gameObject.SetActive(true);
                Destroy(gameObject);

            }
        }

        if (target != null && !isDragging)
        {
            m_trigger = true;

        }

    }
}
