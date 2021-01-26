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

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "snakeHead" && sg.snakelength >= 6)
        {
            SceneManager.LoadScene("Level2");
        }
    }
}
