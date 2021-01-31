using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class foodGenerator : MonoBehaviour
{
    positionRecord foodPosition;
    snakeGenerator sn;

    GameObject foodObject;
    GameObject enemySnake;

    public List<positionRecord> allTheFood;

    bool hasSpawned = false;
    bool generateEnemey = false;


    int getVisibleFood()
    {
        int counter = 0;
        foreach(positionRecord f in allTheFood)
        {
            if (f.BreadcrumbBox != null)
            {
                counter++;
            }
        }
        return counter;
    }

    public void eatFood(Vector3 snakeHeadPosition)
    {
        positionRecord snakeHeadPos = new positionRecord();
        Debug.Log(snakeHeadPosition);

        snakeHeadPos.Position = snakeHeadPosition;

        int foodIndex = allTheFood.IndexOf(snakeHeadPos);
        Debug.Log(allTheFood.Count);
   

        if (foodIndex != -1)
        { 

            Color foodColor;

            foodColor = allTheFood[foodIndex].BreadcrumbBox.GetComponent<SpriteRenderer>().color;

            sn.changeSnakeColor(sn.snakelength,foodColor);

            Destroy(allTheFood[foodIndex].BreadcrumbBox);

            allTheFood.RemoveAt(foodIndex);

            sn.snakelength++;
        }
    }


    public IEnumerator generateFood()
    {
        while(true)
        {
            if (getVisibleFood() < 6) {
                yield return new WaitForSeconds(1f/*(Random.Range(1f, 3f)*/);

                foodPosition = new positionRecord();

                float randomX = Mathf.Floor(Random.Range(-9f, 9f));

                float randomY = Mathf.Floor(Random.Range(-9f, 9f));

                Vector3 randomLocation = new Vector3(randomX, randomY);

                //don't allow the food to be spawned on other food
                foodPosition.Position = randomLocation;

                if (!allTheFood.Contains(foodPosition) && !sn.hitTail(foodPosition.Position,sn.snakelength))
                {
                    
                    if(Physics2D.OverlapCircleAll(randomLocation, 0.1f).Length == 0)
                    {
                        foodPosition.BreadcrumbBox = Instantiate(foodObject, randomLocation, Quaternion.Euler(0f, 0f, 45f));

                        foodPosition.BreadcrumbBox.transform.parent = GameObject.Find("FoodParent").transform;

                        //make the food half the size
                        foodPosition.BreadcrumbBox.transform.localScale = new Vector3(0.5f, 0.5f);


                        foodPosition.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

                        foodPosition.BreadcrumbBox.transform.localScale = new Vector3(0.5f, 0.5f);

                        foodPosition.BreadcrumbBox.name = "Food Object";

                        allTheFood.Add(foodPosition);
                    }

                    
                }
                yield return null;
            }
            if(allTheFood.Count == 6)
            {
                generateEnemey = true;
                yield return null;
            }
            
            
            yield return null;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        sn = Camera.main.GetComponent<snakeGenerator>();

        foodPosition = new positionRecord();

        allTheFood = new List<positionRecord>();

        foodObject = Resources.Load<GameObject>("Prefabs/Food");

        enemySnake = Resources.Load<GameObject>("Prefabs/Enemy");

        StartCoroutine(TransformFoodTimer());
    }


    IEnumerator TransformFoodTimer()
    {
        if (SceneManager.GetActiveScene().name == "Level2" || SceneManager.GetActiveScene().name == "Level3")
        {
            while (true)
            {
                if (generateEnemey && !hasSpawned)
                {
                    yield return new WaitForSeconds(4f);
                    StartCoroutine(TransformFood());
                    generateEnemey = false;
                }
                yield return null;
            }
        }
    }


    IEnumerator TransformFood()
    {
        Transform foodChild = GameObject.Find("FoodParent").transform;

        int randomObj = Random.Range(0, allTheFood.Count - 1);

        GameObject food = allTheFood[randomObj].BreadcrumbBox;
        Vector3 childPos = food.transform.position;

        Destroy(food);
        Instantiate(enemySnake, childPos, Quaternion.identity);

        hasSpawned = true;
    
        yield return null;
    }
}
