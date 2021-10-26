using UnityEngine;
using UnityEngine.Playables;
public class SlowTrap : AOETrap
{
    public Slow slow;
    private float m_dur;
    PlayableDirector m_director;
    private void Start()
    {
        m_dur = slow.slowDuration;
        m_director = GetComponent<PlayableDirector>();
        
    }
    private void Update()
    {
        if (!isDragging)
        { 
            m_director.Play();
            m_dur -= Time.deltaTime;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Enemy") && !isDragging)
        {
            if(m_dur > 0)
            {
                
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.DecreaseSpeed(slow.slowSpeed);
            }
            else
            {
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.ResetSpeed();
                Destroy(gameObject);
            }


           
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
}
