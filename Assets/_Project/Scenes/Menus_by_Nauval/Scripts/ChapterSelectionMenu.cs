using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterSelectionMenu : MonoBehaviour
{
    [SerializeField] private Animator chapterAC;

    public void LoadChapter(int index)
    {
        // TODO: Change this if there is multiple chapters. But in the demo, we will only have one chapter.
        StartCoroutine(LoadSceneAfterExit("LevelSelection"));
        //StartCoroutine(LoadSceneAfterExit("Chapter " + index));
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(LoadSceneAfterExit("MainMenu"));
    }

    IEnumerator LoadSceneAfterExit(string scene)
    {
        chapterAC.Play("panel_out", -1, 0f);
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(scene);
    }
}
