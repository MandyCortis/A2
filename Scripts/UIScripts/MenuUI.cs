using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void Quit()
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("Level1");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("StartScreen");
    }
}
