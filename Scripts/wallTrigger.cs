using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wallTrigger : MonoBehaviour
{
    timerCanvas tm;

    private void Start()
    {
        tm = Camera.main.GetComponent<timerCanvas>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        print("triggered");
        if (collision.gameObject.tag == "snakeHead")
        {
            print("collided with wall");
            SceneManager.LoadScene("GameOver");
            Destroy(tm);
        }
        else if(collision.gameObject.tag == "food")
        {
            print("collided with food");
            Destroy(collision.gameObject);
        }
    }
}
