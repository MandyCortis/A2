﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class snakeheadController : MonoBehaviour
{
    foodGenerator myfoodgenerator;

    
    public Vector3 findClosestFood()
    {
        if (myfoodgenerator.allTheFood.Count > 0)
        {
            List<positionRecord> sortedFoods = myfoodgenerator.allTheFood.OrderBy(
        x => Vector3.Distance(this.transform.position, x.Position)
       ).ToList();
            return sortedFoods[0].Position;
        }
        return new Vector3(0f, 0f);
    }

    public IEnumerator automoveCoroutine()
    {
        while(true)
        {
            yield return null;
        }
    }


    private void Start()
    {
        myfoodgenerator = Camera.main.GetComponent<foodGenerator>();
    }

    void checkBounds()
    {
        /*
        if ((transform.position.x < -(Camera.main.orthographicSize-1)) || (transform.position.x > (Camera.main.orthographicSize - 1)))
        {
            transform.position = new Vector3(-transform.position.x,transform.position.y);
        }

        if ((transform.position.y < -(Camera.main.orthographicSize - 1)) || (transform.position.y > (Camera.main.orthographicSize - 1)))
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y);
        }
        */
        if (transform.position.x <= -10f)
        {
            transform.position = new Vector2(-9f, transform.position.y);
        }
        else if (transform.position.x >= 10f)
        {
            transform.position = new Vector2(9f, transform.position.y);
        }

        // Y axis
        if (transform.position.y <= -10f)
        {
            transform.position = new Vector2(transform.position.x, -9f);
        }
        else if (transform.position.y >= 10f)
        {
            transform.position = new Vector2(transform.position.x, 9f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(1f,0);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1f, 0);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 1f);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position -= new Vector3(0, 1f);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
        }
    }
}
