using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    SubmitName submitName;
    public static GameManager Instance { get; private set; }

    public Text scoreText;
    public static int score = 0;

    public static int enemyLength = 4;

    //public float completetimervalue = 0f;
    public string playerName;
    public float time;
    private bool started = false;

    void Awake()
    {
        if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);

        DontDestroyOnLoad(GameObject.FindWithTag("score"));

    }

   

    void Start()
    {
       
        
        //scoreText.text = "Score: " + score.ToString();

        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            //Destroy(this);
        }
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void SetUsername(string username)
    {
        this.playerName = username;
    }

    public string GetUsername()
    {
        return playerName;
    }

    public void StartTimer()
    {
        started = true;
        SceneManager.LoadScene("Level1");
    }

    public float GetTime()
    {
        return time;
    }

    public void Restart()
    {
        playerName = "";
        time = 0;
        SceneManager.LoadScene("StartLevel");
    }
}
