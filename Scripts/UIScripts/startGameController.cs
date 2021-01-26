using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startGameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.GetComponent<snakeGenerator>().enabled = true;
        Camera.main.GetComponent<foodGenerator>().enabled = true;
    }
}
