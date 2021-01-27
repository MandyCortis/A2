using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    timerManager tm;
   

    //Text timerText;

    void Awake()
    {
        //if (Instance == null) { Instance = this; } else if (Instance != this) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //tm = Camera.main.GetComponentInChildren<timerManager>();
        //print(tm.timerValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
