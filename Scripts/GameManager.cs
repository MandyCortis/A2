using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Text txtName;
    public static string Pname;


    public void Awake()
    {
        //if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
    }


    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level1" || SceneManager.GetActiveScene().name == "Level2" || SceneManager.GetActiveScene().name == "Level3")
        {
            txtName = GameObject.Find("name").GetComponent<Text>();
            txtName.text = Pname;
        }
    }
}
