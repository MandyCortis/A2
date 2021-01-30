using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    SubmitName submit;
    public string name;
    public void OnStartGame(string level1)
    { 
        SceneManager.LoadScene(level1);
        //submit.GetName();
    }

    public void SetName(string name)
    {
        this.name = name;
    }
}
