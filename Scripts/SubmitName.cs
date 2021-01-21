using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitName : MonoBehaviour
{
    private string name = null;
    public void OnValueChange()
    {
        InputField inputField = GameObject.Find("UsernameField").GetComponent<InputField>();
        string name = inputField.text;

        if(name.Length >= 3)
        {
            GameObject.Find("StartButton").GetComponent<Button>().interactable = true;
        }

        if(name.Length < 3)
        {
            GameObject.Find("StartButton").GetComponent<Button>().interactable = false;
        }
    }

    public void OnEndEdit()
    {
        InputField inputField = GameObject.Find("UsernameField").GetComponent<InputField>();
        name = inputField.text;

        GameObject.Find("StartSceneScripts").GetComponent<StartGame>().SetName(name);
    }

    public string GetName()
    { 
        return name;
    }
}
