using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultScreenManager : MonoBehaviour
{

    public GameObject ResultScreen;

    /*  public void ShowResultScreen()
      {
          ResultScreen.SetActive(true);
      }
  */
    public void ShowResultScreen()
    {
        StartCoroutine(showResult(2));
    }

    IEnumerator showResult(float time)
    {
        yield return new WaitForSeconds(2);
        ResultScreen.SetActive(true);
    }
}
