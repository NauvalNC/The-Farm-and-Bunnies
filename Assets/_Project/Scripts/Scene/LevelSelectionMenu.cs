using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    [SerializeField] private Animator levelMenuAC;
    [SerializeField] private Transform levelPanelCont;
    [SerializeField] private GameObject levelPanelPrefab;

    [Header("Demo Settings")]
    [SerializeField] private int numOfDemoLevels = 3;
    [SerializeField] private Color completedStarColor, incompletedStarColor;

    private void Start()
    {
        LoadLevelPanels();
 
    }

    private void LoadLevelPanels()
    {

        // TODO: Change this to load actual levels.
        // But in this demo, I will only load two level, which is chapter 1 lv1-2.
        GameObject m_temp;
        Transform m_starCont;
        Text m_lvNumber;
        int chapter = PlayerPrefs.GetInt("Chapter");
        print(chapter);
        for (int i = 0; i < numOfDemoLevels; i++)
        {
            m_temp = Instantiate(levelPanelPrefab, levelPanelCont);
            m_temp.transform.localScale = Vector3.one;

            m_lvNumber = m_temp.transform.Find("level_number").GetComponent<Text>();
            m_lvNumber.text = (i + 1).ToString();

            // Color the stars based on level completion
            m_starCont = m_temp.transform.Find("star_cont");
            int levelScore = PlayerPrefs.GetInt("C" + PlayerPrefs.GetInt("Chapter") + "_L"+ (i));

            if (i == 0 || levelScore != 0)
            {
                m_temp.transform.Find("lock_panel").gameObject.SetActive(false);
                Button m_button = m_temp.GetComponent<Button>();
                m_button.interactable = true;
                int m_lvIndex = i + 1;
                m_button.onClick.AddListener(delegate { LoadLevel(m_lvIndex); });
            }

            else
            {
                m_temp.GetComponent<Button>().interactable = false;
            }

            //TOLONG fix caranya biar nggak double di atas 
            levelScore = PlayerPrefs.GetInt("C" + PlayerPrefs.GetInt("Chapter") + "_L"+ (i+1));
            foreach (Transform m_star in m_starCont)
            {
                if (levelScore >= 1)
                {
                    m_star.GetComponent<Image>().color = completedStarColor;
                    levelScore--;
                }
                else
                {
                    m_star.GetComponent<Image>().color = incompletedStarColor;
                }
            }

            // TEST UNLOCK LEVEL
           
            

            // If the level is level 1-2 (which is the demo) then set lock to available
            // if (i + 1 <= 2)
            // {
            //     m_temp.transform.Find("lock_panel").gameObject.SetActive(false);
            //     Button m_button = m_temp.GetComponent<Button>();
            //     m_button.interactable = true;
            //     int m_lvIndex = i + 1;
            //     m_button.onClick.AddListener(delegate { LoadLevel(m_lvIndex); });
            // } 
            // else
            // {
            //     m_temp.GetComponent<Button>().interactable = false;
            // }
        }
    }

    public void LoadLevel(int index)
    {
        AudioManager.instance.Play("Button");
        PlayerPrefs.SetInt("Level", index);
        StartCoroutine(LoadSceneAfterExit("C" + PlayerPrefs.GetInt("Chapter") + "_L"+ index));
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(LoadSceneAfterExit("ChapterSelection"));
    }

    IEnumerator LoadSceneAfterExit(string scene)
    {
        levelMenuAC.Play("panel_out", -1, 0f);
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(scene);
    }
}
