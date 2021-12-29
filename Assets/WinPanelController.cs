using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanelController : MonoBehaviour
{
    [Header("CarrotScore")]
    [SerializeField]
    private Sprite m_color_carrot;

    [SerializeField]
    private GameObject m_carrot_list;

    private void OnEnable()
    {
        for(int i = 0; i < GameManager.Instance.score; i++)
        {
            m_carrot_list.transform.GetChild(i).GetComponent<Image>().sprite = m_color_carrot;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
