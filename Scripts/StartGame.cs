using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    string name;
    public void OnStartGame(string level1)
    { 
        SceneManager.LoadScene(level1);

        //Enter where to send things here



        //on start new game, gamemanager all values must be default

    }

    public void SetName(string name)
    {
        this.name = name;
    }
}
