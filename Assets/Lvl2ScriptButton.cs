using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl2ScriptButton : MonoBehaviour
{
    public void OnPressStartButton()
    {
        SceneManager.LoadScene("Level 2");
    }
}
