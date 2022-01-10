using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIGameManager : MonoBehaviour
{
    public static UIGameManager instance;


    [Header("Result Screen")]
    [SerializeField]
    private GameObject win_panel;
    [SerializeField]
    private GameObject lose_panel;
    [SerializeField]
    private GameObject overlay_panel;
    [SerializeField]
    private Animator comingSoonAC;

    [Header("Pause Menu")]
    [SerializeField]
    private GameObject pause_menu;

    [Header("Ads Menu")]
    [SerializeField]
    private GameObject ads_menu;
    [SerializeField]
    private Text m_countDownText;

    bool m_countDown = false;
    float m_startCountdown = 3;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        m_countDownText = overlay_panel.transform.GetChild(0).GetComponent<Text>();
    }
    private void Update()
    {
        if (m_countDown)
        {
            m_startCountdown -= Time.deltaTime;
            if(m_startCountdown <= 0)
            {
                m_startCountdown = 3;
                m_countDown = false;
            }
        }
        m_countDownText.text = Mathf.Round(m_startCountdown).ToString();
    }
    public void Pause()
    {
        pause_menu.SetActive(true);
        AudioManager.instance.Play("Button");
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        AudioManager.instance.Play("Button");
        pause_menu.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        AudioManager.instance.Play("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        AudioManager.instance.Play("Button");
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        try
        {
            Time.timeScale = 1;
            AudioManager.instance.Play("Button");
            if (SceneManager.GetActiveScene().name == "C1_L3")
            {
                throw new System.Exception();
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        catch (System.Exception e)
        {
            comingSoonAC.Play("panel_in", -1, 0f);
            Debug.Log(e.Message);
        }
    }

    public void ShowWinPanel()
    {

        overlay_panel.SetActive(true);
        win_panel.SetActive(true);
    }

    public void ShowLosePanel()
    {

        overlay_panel.SetActive(true);
        lose_panel.SetActive(true);
    }

    public void ShowAdPanel()
    {
        AudioManager.instance.Play("Button");
        ads_menu.SetActive(true);
    }


    public void Revive()
    {
        AudioManager.instance.Play("Button");
        lose_panel.SetActive(false);
        ads_menu.SetActive(false);
        StartCoroutine(ResumeRevive());

    }

    IEnumerator ResumeRevive()
    {
        Time.timeScale = 1;
        m_countDown = true;
        m_countDownText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        m_countDownText.gameObject.SetActive(false);
        overlay_panel.SetActive(false);
        GameManager.Instance.Revive();
        m_countDown = false;
        m_startCountdown = 3;
    }
}
