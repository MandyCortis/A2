using UnityEngine;
using UnityEngine.UI;

public class SubmitName : MonoBehaviour
{
    public InputField usernameInput;
    public static string name;

    public void Start()
    {
        
    }

    public void OnValueChange()
    {
        usernameInput = GameObject.Find("UsernameField").GetComponent<InputField>();
        name = usernameInput.text.ToString();

        if (name.Length >= 3)
        {
            GameObject.Find("StartButton").GetComponent<Button>().interactable = true;

           // usernameInput.text = name;
        }

        if(name.Length < 3)
        {
            GameObject.Find("StartButton").GetComponent<Button>().interactable = false;
        }

        //GameManager.Instance.name = name;
    }

    public void OnEndEdit()
    {
        InputField inputField = GameObject.Find("UsernameField").GetComponent<InputField>();
        name = inputField.text;

        //GameObject.Find("StartSceneScripts").GetComponent<StartGame>().SetName(name);
        //GameManager.Instance.name = name;
    }

    /*
    public void SaveUser()
    {
        name = inputField.GetComponent<Text>().text; 
    }
    
    */
    public string GetName()
    { 
        return name;
    }
    
}
