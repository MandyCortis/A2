using UnityEngine;
using UnityEngine.UI;

public class SubmitName : MonoBehaviour
{
    public InputField usernameInput;
    public static string name;


    public void OnValueChange()
    {
        usernameInput = GameObject.Find("UsernameField").GetComponent<InputField>();
        name = usernameInput.text.ToString();

        if (name.Length >= 3)
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
    }

   
    public string GetName()
    { 
        return name;
    }
}
