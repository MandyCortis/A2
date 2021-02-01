using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class timerManager : MonoBehaviour
{
    public bool timerStarted;

    public static float timerValue=0f;

    public static Text timerText;

    public string timerString;


    void Start()
    {
        timerText = GetComponent<Text>();
        StartCoroutine(timer());
        timerString = timerText.ToString();
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

    public static void ResetTimer()
    {
        timerValue = 0f;
        timerText.text = string.Format("{0:00}:{1:00}", 0f, 0f);
        Destroy(GameObject.FindWithTag("timer"));
    }
}
