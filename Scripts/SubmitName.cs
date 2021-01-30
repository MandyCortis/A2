using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitName : MonoBehaviour
{
    //public InputField usernameInput;
    public static string name;

    public void OnValueChange()
    {
        InputField inputField = GameObject.Find("UsernameField").GetComponent<InputField>();
        name = inputField.text;

        if(name.Length >= 3)
        {
            GameObject.Find("StartButton").GetComponent<Button>().interactable = true;
           // usernameInput.text = name;
        }

        if(name.Length < 3)
        {
            GameObject.Find("StartButton").GetComponent<Button>().interactable = false;
        }

        GameManager.Instance.name = name;
    }

    public void OnEndEdit()
    {
        InputField inputField = GameObject.Find("UsernameField").GetComponent<InputField>();
        name = inputField.text;

        GameObject.Find("StartSceneScripts").GetComponent<StartGame>().SetName(name);
        GameManager.Instance.name = name;
    }

    /*
    public void SaveUser(string newName)
    {
        name = newName;
    }
    */
    
    public string GetName()
    { 
        return name;
    }
    
}
