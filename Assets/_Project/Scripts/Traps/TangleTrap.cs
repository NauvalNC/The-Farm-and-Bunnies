using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TangleTrap : SingleTargetTrap
{
    public Tangle tangleStats;
    float m_tangleDur;

    private void Start()
    {
        m_tangleDur = tangleStats.tangleDuration;
    }
    // Update is called once per frame
    void Update()
    {


        if(target != null && !isDragging)
        {
            if (m_tangleDur > 0)
            {
  
                target.GetComponent<Enemy>().Tangle();
                m_tangleDur -= Time.deltaTime;

            }
            else
            {
                target.GetComponent<Enemy>().ResetSpeed();
                Destroy(gameObject);

            }
        }

    }
}
