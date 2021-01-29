using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static int enemyLength = 4;

    void Awake()
    {
        //if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
    }

   

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            Destroy(this);
        }
    }

    void Update()
    {
        //DestroyTimer();
    }
}
