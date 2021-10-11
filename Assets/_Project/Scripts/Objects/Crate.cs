using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crate : MonoBehaviour
{
    [SerializeField]
    protected int numberOfItems = 10;
    int m_qtyItems;

    [Header("Renderer")]
    [SerializeField]
    protected Text quantityText;

    private void Awake()
    {
        m_qtyItems = numberOfItems;    
    }

    private void Update()
    {
        UpdateGraphics();
    }

    void UpdateGraphics()
    {
        quantityText.text = m_qtyItems + "/" + numberOfItems;
    }

    public void Stealed()
    {
        m_qtyItems--;
    }

    public void Returned()
    {
        m_qtyItems++;
    }

    public int GetNumberOfItems() { return numberOfItems; }

    public int GetActualNumberOfItems() { return m_qtyItems; }
}
