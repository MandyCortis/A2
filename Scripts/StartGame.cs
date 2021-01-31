using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public InputField usernameInput;
    

    void Start()
    {
       
    }

    public void OnStartGame(string level1)
    {
        usernameInput = GameObject.Find("UsernameField").GetComponent<InputField>();
        GameObject.Find("GameManager").GetComponent<GameManager>().Pname = usernameInput.text.ToString();
        print(GameObject.Find("GameManager").GetComponent<GameManager>().Pname);

        SceneManager.LoadScene(level1);
    }

    /*
    public void SetName(string name)
    {
        this.name = name;
    }
    */
}
