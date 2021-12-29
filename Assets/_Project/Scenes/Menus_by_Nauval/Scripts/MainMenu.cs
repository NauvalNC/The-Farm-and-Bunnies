using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator infoAC, exitAC, mainAC;
    private bool isInfoOpen = false, isExitOpen = false;

    public void PlayGame()
    {
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
