using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl1Scriptbutton : MonoBehaviour
{
    public void OnPressStartButton()
    {
        SceneManager.LoadScene("Trap");
    }
}
