using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChapterSelectionMenu : MonoBehaviour
{
    [SerializeField] private Animator chapterAC;
    [SerializeField] private Transform chapterPanelCont;
    [SerializeField] private int numberOfChapters = 5;

    private void Start() {
        UnlockLevels();
    }

    public void UnlockLevels()
    {   
        
        //misalnya mau buka chapter selanjutnya di level 2
        int levelIndex = 2;
        //1 -> Chapter 1, etc
        int chapterIndex = 0;
        foreach (Transform chapterPanels in chapterPanelCont)
        {
            int levelScore = PlayerPrefs.GetInt("C" + (chapterIndex) + "_L"+ (levelIndex));
            if (chapterIndex >= 1 && levelScore != 0)
            {
                Transform chapterPanel = chapterPanelCont.transform.GetChild(chapterIndex);
                Transform m_icon_mask = chapterPanel.transform.Find("icon_mask");
                m_icon_mask.transform.Find("lock_icon").gameObject.SetActive(false);
                Button m_button = chapterPanels.GetComponent<Button>();
                m_button.interactable = true;
                int m_chapterIndex = chapterIndex + 1;
                m_button.onClick.AddListener(delegate { LoadChapter(m_chapterIndex); });
            }
            chapterIndex++;
        }
    }
     
    public void LoadChapter(int index)
    {
        PlayerPrefs.SetInt("Chapter", index);
        StartCoroutine(LoadSceneAfterExit("LevelSelection"));
    }

    // public void LoadChapter(int index)
    // {
    //     // TODO: Change this if there is multiple chapters. But in the demo, we will only have one chapter.
    //     PlayerPrefs.SetInt("Chapter", index);
    //     StartCoroutine(LoadSceneAfterExit("LevelSelection"));
    //     //StartCoroutine(LoadSceneAfterExit("Chapter " + index));
    // }

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
