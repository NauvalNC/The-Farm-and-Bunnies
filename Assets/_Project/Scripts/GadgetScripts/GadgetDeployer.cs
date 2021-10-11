using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GadgetDeployer : MonoBehaviour
{
    static GadgetDeployer instance;
    public static GadgetDeployer Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GadgetDeployer>();
            return instance;
        }
    }

    [Header("General")]
    public float initDeploymentPoint;
    public float maxDeploymentPoint;
    public float dpRecoveryPerSecond;
    float m_deploymentPoint;
    int m_actualDP;

    [Header("Renderers")]
    [SerializeField] protected Text dpText;
    [SerializeField] protected GameObject gadgetIconPrefab;
    [SerializeField] protected GameObject gadgetLister;

    [Header("Gadgets")]
    public List<GadgetDetails> gadgets;

    private void Awake()
    {
        SetupAttributes();
    }

    private void Update()
    {
        DeploymentPointManager();
        UpdateGraphics();
    }

    void SetupAttributes()
    {
        m_deploymentPoint = initDeploymentPoint;
        foreach (GadgetDetails gd in gadgets) AddGadget(gd);
    }

    void DeploymentPointManager()
    {
        m_deploymentPoint += dpRecoveryPerSecond * Time.deltaTime;
        if (m_deploymentPoint >= maxDeploymentPoint) m_deploymentPoint = maxDeploymentPoint;
        else if (m_deploymentPoint <= 0) m_deploymentPoint = 0;
        m_actualDP = (int)m_deploymentPoint;
    }

    void UpdateGraphics()
    {
        dpText.text = m_actualDP.ToString();
    }

    void AddGadget(GadgetDetails gadget)
    {
        GameObject m_temp = Instantiate(gadgetIconPrefab, gadgetLister.transform);
        m_temp.transform.localScale = Vector3.one;
        m_temp.GetComponent<GadgetItem>().gadget = gadget;
    }

    public int GetCurrentDP() { return m_actualDP; }

    public void UseDP(int amount) { m_deploymentPoint -= amount; }
}

[System.Serializable]
public class GadgetDetails
{
    public Gadget gadget;
    public int qty;
}