using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class enemyPositionRecord
{
    //the place where I've been
    Vector3 position;
    //at which point was I there?
    int positionOrder;


    GameObject breadcrumbBox;

    public void changeColor()
    {
        this.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.black;
    }


    public Vector3 Position { get => position; set => position = value; }
    public int PositionOrder { get => positionOrder; set => positionOrder = value; }
    public GameObject BreadcrumbBox { get => breadcrumbBox; set => breadcrumbBox = value; }
}


public class spawnEnemy : MonoBehaviour
{
    public int enemySnakeLength = 4;

    public GameObject[] foods;

    foodGenerator fg;

    [Header("Snake Spawn Position")]


    GameObject enemyBox, breadcrumbBox, pathParent, enemySnake;

    List<enemyPositionRecord> pastPositions;

    int positionorder = 0;

    bool firstrun = true;


    // Start is called before the first frame update
    void Start()
    {
        enemySnake = new GameObject("Enemy Snake");
        enemySnake.transform.position = new Vector3(0f, 0f);

        foods = GameObject.FindGameObjectsWithTag("Food");

       

        pathParent = new GameObject();

        pathParent.transform.position = new Vector3(0f, 0f);

        pathParent.name = "Path Parent";


        breadcrumbBox = Resources.Load<GameObject>("Prefabs/Enemy");
        enemyBox.GetComponent<SpriteRenderer>().color = Color.red;

        //move the snakehead automatically with pathing
        enemyBox.AddComponent<customAIMoveScript>();
        enemyBox.AddComponent<LineRenderer>();

        enemyBox.name = "Enemy";

        enemyBox.transform.SetParent(enemySnake.transform);

        pastPositions = new List<enemyPositionRecord>();
    }


    void Update()
    {

    }


    // Update is called once per frame

    bool boxExists(Vector3 positionToCheck)
    {
        //foreach position in the list

        foreach (enemyPositionRecord p in pastPositions)
        {

            if (p.Position == positionToCheck)
            {
                Debug.Log(p.Position + "Actually was a past position");
                if (p.BreadcrumbBox != null)
                {
                    Debug.Log(p.Position + "Actually has a red box already");
                    //this breaks the foreach so I don't need to keep checking
                    return true;
                }
            }
        }

        return false;
    }


    public void savePosition()
    {
        enemyPositionRecord currentBoxPos = new enemyPositionRecord();

        currentBoxPos.Position = enemyBox.transform.position;
        positionorder++;
        currentBoxPos.PositionOrder = positionorder;

        if (!boxExists(enemyBox.transform.position))
        {
            currentBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, enemyBox.transform.position, Quaternion.identity);

            currentBoxPos.BreadcrumbBox.transform.SetParent(pathParent.transform);

            currentBoxPos.BreadcrumbBox.name = positionorder.ToString();

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 0);

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        pastPositions.Add(currentBoxPos);
        Debug.Log("Have made this many moves: " + pastPositions.Count);

    }


    public void drawTail(int length)
    {
        clearTail();

        if (pastPositions.Count > length)
        {
            //nope
            //I do have enough positions in the past positions list
            //the first block behind the player
            int tailStartIndex = pastPositions.Count - 1;
            int tailEndIndex = tailStartIndex - length;


            //if length = 4, this should give me the last 4 blocks
            for (int snakeblocks = tailStartIndex; snakeblocks > tailEndIndex; snakeblocks--)
            {

                Debug.Log(snakeblocks);

                pastPositions[snakeblocks].BreadcrumbBox = Instantiate(breadcrumbBox, pastPositions[snakeblocks].Position, Quaternion.identity);
                pastPositions[snakeblocks].BreadcrumbBox.GetComponent<SpriteRenderer>().color = new Color32(60, 0, 200, 100);
                pastPositions[snakeblocks].BreadcrumbBox.transform.SetParent(enemySnake.transform);


            }

        }

        if (firstrun)
        {
            int curLength = 0;

            for (curLength = 0; curLength < length; curLength++)
            {
                for (int count = curLength; count < length; count++)
                {
                    enemyPositionRecord fakeBoxPos = new enemyPositionRecord();
                    fakeBoxPos.Position = new Vector3(-9f, 9f);

                    pastPositions.Add(fakeBoxPos);
                }
                drawTail(curLength);
            }



            firstrun = false;



        }

    }
    void clearTail()
    {
        foreach (enemyPositionRecord p in pastPositions)
        {
            Destroy(p.BreadcrumbBox);
        }
    }
*/

