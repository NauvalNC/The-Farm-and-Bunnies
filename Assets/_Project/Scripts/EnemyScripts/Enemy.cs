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
    public float speed = 4f;
    public int maxHealth = 20;
    int m_health;
    NavMeshAgent m_agent;
    Crate m_crate;

    [Header("Theft Settings")]
    public Transform portal;
    bool m_isGotItem = false;

    [Header("Renderer Settings")]
    public GameObject statusFlag;

    private void Awake()
    {
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
        m_agent.speed = speed;

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
        targetDes = null;

        if (m_temp == portal)
        {
            OnManageToSteal();
        } else
        {
            Debug.Log("Item stealed, back to portal!");
            
            m_crate = m_temp.GetComponent<Crate>();
            m_crate.Stealed();

            m_isGotItem = true;
            SetDestination(portal);
        }
    }

    void OnManageToSteal()
    {
        Debug.Log("Item successfully stealed!");
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


}
