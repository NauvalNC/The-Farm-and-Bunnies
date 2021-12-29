using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("Agent Settings")]
    public Transform targetDes;
    public float stoppingDstMultiplier = 2f;
    public float initSpeed = 4f;
    public int maxHealth = 20;

    bool m_isSlow;
    float m_speed = 4f;
    int m_health;

    [SerializeField]
    Transform m_crateTransform;

    NavMeshAgent m_agent;
    Crate m_crate;

    [Header("Theft Settings")]
    public Transform portal;
    bool m_isGotItem = false;

    [Header("Renderer Settings")]
    public GameObject statusFlag;

    private void Awake()
    {
        m_speed = initSpeed;
        m_health = maxHealth;
        m_agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
     
        RunAgent();
        UpdateGraphics();
 
    }

    void UpdateGraphics()
    {
        statusFlag.SetActive(m_isGotItem);
    }

    void RunAgent()
    {
        m_agent.speed = m_speed;

        // Try to reach destination
        if (targetDes != null)
        {
            float m_dis = Vector3.Distance(m_agent.transform.position, targetDes.transform.position);
            float m_bound = (targetDes.transform.lossyScale.x * targetDes.transform.lossyScale.z) / 3f;
            float m_threshold = stoppingDstMultiplier * m_bound;

            if (m_dis <= m_threshold)
            {
                m_agent.isStopped = true;
                OnDestinationReached();
            }
            else
            {
                m_agent.isStopped = false;
                m_agent.SetDestination(targetDes.transform.position);
            }
        }
    }

    void OnDestinationReached()
    {
        Transform m_temp = targetDes;

        if(!m_isGotItem) m_crateTransform = m_temp;
        
        targetDes = null;


        if (m_temp == portal)
        {
            OnManageToSteal();
        } else
        {

            m_isGotItem = true;
            SetDestination(portal);
        }
    }

    void OnManageToSteal()
    {
        m_crate = m_crateTransform.GetComponent<Crate>();
        m_crate.Stealed();
        Destroy(gameObject);
    }

    public void SetDestination(Transform targetDes)
    {
        this.targetDes = targetDes;
    }

    public void OnDamage(int damage)
    {
        m_health -= damage;
        if(m_health <= 0)
        {
            //enemy death
            Destroy(gameObject);
        }
    }

    public void ResetSpeed()
    {
        m_isSlow = false;
        m_speed = initSpeed;
    }

    public void DecreaseSpeed(int speed)
    {
        //prevent slow speed stacking
        if (m_isSlow) return;

        m_speed -= speed;
        m_isSlow = true;
    }

    public void Tangle()
    {
        m_speed = 0;
    }
}
