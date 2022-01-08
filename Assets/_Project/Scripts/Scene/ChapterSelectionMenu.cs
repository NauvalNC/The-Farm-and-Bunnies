using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChapterSelectionMenu : MonoBehaviour
{
    [SerializeField] private Animator chapterAC;
    [SerializeField] private Transform chapterPanelCont;
    [SerializeField] private int numberOfChapters;
    [SerializeField] private Color m_panelColor;
    [SerializeField] private Color m_textColor;
    private void Start() {
        //UnlockLevels();
       
    }

    public void UnlockLevels()
    {   
        
        //last level in each chapter
        int levelIndex = 3;
 
        
        for(int i = 0; i < numberOfChapters; i++)
        {
            int lastLevelScore = PlayerPrefs.GetInt("C" + (i+1) + "_L" + (levelIndex));
            if(lastLevelScore > 0)
            {
                Transform chapterPanel = chapterPanelCont.transform.GetChild(i + 1);
                Transform m_icon_mask = chapterPanel.transform.GetChild(2);
                Transform col_icon_mask = m_icon_mask.GetChild(0);
                chapterPanel.GetChild(0).GetComponent<Text>().color = m_textColor;
                chapterPanel.GetChild(1).GetComponent<Text>().color = m_textColor;
                col_icon_mask.GetComponent<Image>().color = Color.white;
                chapterPanel.GetComponent<Image>().color = m_panelColor;
                m_icon_mask.transform.Find("lock_icon").gameObject.SetActive(false);
                Button m_button = chapterPanel.GetComponent<Button>();
                m_button.interactable = true;
   
            }
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
