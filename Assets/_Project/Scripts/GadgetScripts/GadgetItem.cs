using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GadgetItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public GadgetDetails gadget;
    GameObject m_tmpObj;

    [Header("Renderers")]
    [SerializeField] protected Image gadgetIcon;
    [SerializeField] protected Text quantityTxt, costTxt, cdText;
    [SerializeField] protected Image cdPanel;
    [SerializeField] protected GameObject notAvailablePanel;

    float m_cdTimer;
    bool m_isInCoolDown = false;
    GameObject m_pool;
    
    void Start()
    {
        m_pool = new GameObject();
        m_pool.transform.position = Vector3.zero;
        m_pool.name = gadget.gadget.gadgetName + "_pool";
    }

    void Update()
    {
        ManageCoolDown();
        UpdateGraphics();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsLegalToSpawn()) return;

        GameManager.Instance.GainTopPriority(gameObject);
        m_tmpObj = GetPool();
        if (m_tmpObj == null)
        {
            m_tmpObj = Instantiate(gadget.gadget.prefab, Vector3.zero, Quaternion.identity);
            m_tmpObj.GetComponent<Trap>().isDragging = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_tmpObj == null) return;

        RaycastHit m_hit;
        Ray m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(m_ray, out m_hit, Mathf.Infinity, 1 << 6))
        {
            m_tmpObj.transform.position = new Vector3(m_hit.point.x, m_hit.point.y + 0.1f, m_hit.point.z);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameManager.Instance.ReleaseTopPriority(gameObject);
        if (m_tmpObj == null) return;

        m_tmpObj.transform.SetParent(m_pool.transform);
        m_tmpObj.GetComponent<Trap>().isDragging = false;
        m_tmpObj = null;


        m_cdTimer = gadget.gadget.CD;
        GadgetDeployer.Instance.UseDP(gadget.gadget.DPCost);
        
        gadget.qty--;
        if (gadget.qty <= 0) Destroy(gameObject);
    }

    GameObject GetPool()
    {
        foreach(Transform m_child in m_pool.transform)
        {
            if (m_child.gameObject.activeInHierarchy == false)
            {
                m_child.gameObject.SetActive(true);
                return m_child.gameObject;
            }
        }

        return null;
    }

    bool IsLegalToSpawn()
    {
        if (m_isInCoolDown || gadget.qty <= 0 || GadgetDeployer.Instance.GetCurrentDP() < gadget.gadget.DPCost) return false;
        return true;
    }

    void ManageCoolDown()
    {
        if (m_cdTimer > 0) m_cdTimer -= Time.deltaTime;
        else m_cdTimer = 0;

        m_isInCoolDown = (m_cdTimer > 0) ? true : false;
    }

    void UpdateGraphics()
    {
        notAvailablePanel.SetActive(!IsLegalToSpawn());

        cdPanel.gameObject.SetActive(m_isInCoolDown);
        cdPanel.fillAmount = m_cdTimer / gadget.gadget.CD;
        cdText.text = m_cdTimer.ToString("n2") + "s";
        
        gadgetIcon.sprite = gadget.gadget.gadgetIcon;
        quantityTxt.text = gadget.qty + "x";
        costTxt.text = gadget.gadget.DPCost.ToString();
    } 
}
