using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonMenu : MonoBehaviour
{
    public void OnPressStartButton()
    {
        SceneManager.LoadScene("level_select");
    }
}
