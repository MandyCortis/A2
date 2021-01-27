using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerCanvas : MonoBehaviour
{
    GameObject timerUI;

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
