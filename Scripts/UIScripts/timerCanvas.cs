﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class timerCanvas : MonoBehaviour
{
    public GameObject timerUI;
    timerManager tm;

    private void Start()
    {
        createCanvas();
    }

    public void createCanvas()
    {
        timerUI = Instantiate(Resources.Load<GameObject>("Prefabs/Timer"), new Vector3(0f, 0f), Quaternion.identity);

        //the default value for the timer is started
        timerUI.GetComponentInChildren<timerManager>().timerStarted = true;
    }
}
