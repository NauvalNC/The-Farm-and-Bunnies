using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class HUDManager : MonoBehaviour
{
    static HUDManager instance;
    public GameObject ResultScreen;
    public GameObject HUD;
    public static HUDManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<HUDManager>();
            return instance;
        }
    }

    Animator m_animator;

    [Header("User Interface")]
    public Text highlightTitle;
    public Text highlightSubTitle;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();    
    }

    /// <summary>
    /// Get HUD animator
    /// </summary>
    /// <returns></returns>
    public Animator GetHUDAC() { return m_animator; }

    public void StartWave()
    {
        GameManager.Instance.StartNextWave();
    }

    public void hideHUD()
    {
        StartCoroutine(hide(2));
    }

    IEnumerator hide(float time)
    {
        yield return new WaitForSeconds(2);
        HUD.SetActive(false);
    }
 
}