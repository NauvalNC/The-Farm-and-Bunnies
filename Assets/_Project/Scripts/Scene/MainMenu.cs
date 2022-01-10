using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator infoAC, exitAC, mainAC;
    private bool isInfoOpen = false, isExitOpen = false;

    [SerializeField]
    private Button[] buttons;

    private void Start()
    {
        int len = buttons.Length;
        for (int i = 0; i < len; i++)
        {
            buttons[i].onClick.AddListener(delegate { AudioManager.instance.Play("button"); });
        }
    }

    public void PlayGame()
    {
        AudioManager.instance.Play("button");
        StartCoroutine(IPlay());
    }

    public void ToggleInfoPanel()
    {
        isInfoOpen = !isInfoOpen;
        infoAC.Play(isInfoOpen ? "panel_in" : "panel_out", -1, 0f);
    }

    public void ToggleExitPanel()
    {
        isExitOpen = !isExitOpen;
        exitAC.Play(isExitOpen ? "panel_in" : "panel_out", -1, 0f);
    }

    public void ConfirmExit()
    {
        Application.Quit();
    }

    IEnumerator IPlay()
    {
        mainAC.Play("menu_out", -1, 0f);
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("ChapterSelection");
    }
}
