using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Pause Menu")]
    [SerializeField]
    private GameObject pause_menu;

    [Header("Ads Menu")]
    [SerializeField]
    private GameObject ads_menu;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
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
        AudioManager.instance.Play("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        ads_menu.SetActive(true);
    }
    
}
