using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class advanceLevel : MonoBehaviour
{
    snakeGenerator sg;

    void Start()
    {
        sg = Camera.main.GetComponent<snakeGenerator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (collision.gameObject.tag == "snakeHead" && sg.snakelength >= 6)
            {
                SceneManager.LoadScene("Level2");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            if (collision.gameObject.tag == "snakeHead" && sg.snakelength >= 6)
            {
                SceneManager.LoadScene("Level3");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            if (collision.gameObject.tag == "snakeHead" && sg.snakelength >= 6)
            {
                Destroy(GameObject.FindWithTag("timer"));
                SceneManager.LoadScene("Win");
            }
        }
    } 
}
