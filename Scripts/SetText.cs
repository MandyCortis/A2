using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    SubmitName submitName;

    [SerializeField] Text playerName;
    [SerializeField] Text time;
    void Start()
    {

        submitName = Camera.main.GetComponent<SubmitName>();

        //playerName = submitName.SaveUser().GetComponent<Text>().text;
        /*
        playerName.text = GameManager.Instance.name;
        time.text = GameManager.Instance.time.ToString("00.00");
        */
    }
    
}
