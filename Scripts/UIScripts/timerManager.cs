using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timerManager : MonoBehaviour
{
    public bool timerStarted;

    public float timerValue=0f;

    public Text highScore;

    public static Text timerText;
    public string timerString;


    void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //the text component attached to THIS object
        timerText = GetComponent<Text>();
        StartCoroutine(timer());
        timerString = timerText.ToString();
        /*
        if (PlayerPrefs.HasKey("HighScore") == true)
        {
            highScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
        else
        {
            highScore.text = "No HighScore yet";
        }
        */
    }



    IEnumerator timer()
    {
        while (true)
        {
            if (timerStarted)
            {
                //measure the time
                timerValue++;

                float minutes = timerValue / 60f;
                float seconds = timerValue % 60f;

                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                //code that is running every second
                yield return new WaitForSeconds(1f);
            }
            else
            {
                //don't measure the time
                timerValue = 0f;
                timerText.text = string.Format("{0:00}:{1:00}", 0f, 0f);
                yield return null;
            }
            
        }
        
    }


    
}
