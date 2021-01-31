using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Text txtName;
    public string Pname;

    public static GameManager Instance { get; private set; }

    public Text scoreText;
    public static int score = 0;

    public int enemyTailLength = 4;


    public float time;
    private bool started = false;

    void Awake()
    {
        if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);

        //DontDestroyOnLoad(GameObject.FindWithTag("score"));

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
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2" || SceneManager.GetActiveScene().name == "Level3")
        {
            txtName = GameObject.Find("name").GetComponent<Text>();
            txtName.text = Pname;
            //scoreText.text = "Score: " + score.ToString();
        }
    }

}
