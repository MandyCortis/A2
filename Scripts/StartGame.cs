using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public InputField usernameInput;
 
    public void OnStartGame(string level1)
    {
        usernameInput = GameObject.Find("UsernameField").GetComponent<InputField>();
        GameManager.Pname = usernameInput.text;
        print(GameManager.Pname);

        SceneManager.LoadScene(level1);
    }
}
