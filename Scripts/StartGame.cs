using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    string name;
    public void OnStartGame(string level1)
    { 
        SceneManager.LoadScene(level1);
    }

    public void SetName(string name)
    {
        this.name = name;
    }
}
