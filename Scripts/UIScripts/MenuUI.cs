using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    timerManager tm;
    timerCanvas tc;

    public void Quit()
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("Level1");
        timerManager.ResetTimer();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("StartScreen");
        timerManager.ResetTimer();
    }
}
