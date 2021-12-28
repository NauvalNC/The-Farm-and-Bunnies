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

    public static int totalCurrItems;
    private void Awake()
    {
        m_qtyItems = numberOfItems;
        totalCurrItems += numberOfItems;
    }

    private void Update()
    {
        if (m_qtyItems <= 0) m_qtyItems = 0;
        if (totalCurrItems <= 0) totalCurrItems = 0;
        UpdateGraphics();
    }

    void UpdateGraphics()
    {
        quantityText.text = m_qtyItems + "/" + numberOfItems;
    }

    public void Stealed()
    {
        totalCurrItems--;
        m_qtyItems--;
    }

    public void Returned()
    {
        m_qtyItems++;
    }

    public int GetNumberOfItems() { return numberOfItems; }

    public int GetActualNumberOfItems() { return m_qtyItems; }
}
