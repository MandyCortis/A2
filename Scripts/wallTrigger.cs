using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wallTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "snakeHead")
        {
            print("collided with wall");
            SceneManager.LoadScene("GameOver");
            timerManager.ResetTimer();
        }
        else if(collision.gameObject.tag == "food")
        {
            print("collided with food");
            Destroy(collision.gameObject);
        }
    }
}
