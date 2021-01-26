using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class wallTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        print("triggered");
        if (collision.gameObject.tag == "snakeHead")
        {
            print("collided with wall");
            SceneManager.LoadScene("GameOver");
            
        }
        else if(collision.gameObject.tag == "food")
        {
            print("collided with food");
            Destroy(collision.gameObject);
        }
    }
}
