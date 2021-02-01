using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;


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


public class customAIMoveScript : MonoBehaviour
{
    //the object that we are using to generate the path
    Seeker seeker;

    //path to follow stores the path
    Path pathToFollow;

    //a reference to the snakeHead
    Transform target;
    //GameObject target;

    //a reference to PointGraphObject
    GameObject graphParent;



    foodGenerator fgen;


    List<enemyPositionRecord> enemyPastPos;
    bool firstrun = true;


    GameObject enemyAI, breadcrumbBox, pathParent;
    int enemyPos = 0;
    int positionOrder = 0;
    public int enemyLength = 4;
    public int targetCounter = 0;
    public int currentTarget;
    public GameObject[] targets;


    LineRenderer lr;
    Color startCol = Color.cyan;
    Color endCol = Color.yellow;

    bool showPath = false;

    void Start()
    {
        //the instance of the seeker attached to this game object
        seeker = GetComponent<Seeker>();

        //setting target's name by tag
        target = GameObject.FindGameObjectWithTag("snakeHead").transform;
        //target = targets[0];
        targets = GameObject.FindGameObjectsWithTag("snakeHead");

        //find the parent node of the grid graph
        graphParent = GameObject.Find("AStarGrid");
        //we scan the graph to generate it in memory
        graphParent.GetComponent<AstarPath>().Scan();

        //update the graph as soon as you can. Runs indefinitely
        StartCoroutine(updateGraph());

        //generate the initial path
        pathToFollow = seeker.StartPath(transform.position, target.transform.position);


        //move the enemy towards the snakeHead
        StartCoroutine(moveTowardsPlayer(this.transform));

        enemyAI = GameObject.FindGameObjectWithTag("enemy");
        breadcrumbBox = Resources.Load<GameObject>("Prefabs/Enemy");

        pathParent = new GameObject();

        pathParent.transform.position = new Vector3(0f, 0f);

        pathParent.name = "Path Parent";


        enemyPastPos = new List<enemyPositionRecord>();

        fgen = Camera.main.GetComponent<foodGenerator>();

        //seeker mode
        lr = this.gameObject.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = startCol;
        lr.endColor = endCol;

        drawTail(enemyLength);
    }

    void Update()
    {
        SeekerMode(this.transform);

        if (Input.GetKey(KeyCode.Space))
        {
            showPath = true;
        }
        if (Input.GetKey(KeyCode.B))
        {
            showPath = false;
        }
    }


    IEnumerator updateGraph()
    {
        while (true)
        {
            graphParent.GetComponent<AstarPath>().Scan();

            yield return null;
        }
    }


    IEnumerator moveTowardsPlayer(Transform t)
    {
        while (true)
        {
            print("entered while 1");
            List<Vector3> posns = pathToFollow.vectorPath;

            for (int counter = 0; counter < posns.Count; counter++)
            {
                print("entered for 2");
                if (posns[counter] != null)
                {
                    print("entered if 3");
                    while (Vector3.Distance(t.position, posns[counter]) >= 0.5f)
                    {
                        print("entered while 4");
                        t.position = Vector3.MoveTowards(t.position, posns[counter], 1f);

                        pathToFollow = seeker.StartPath(t.position, target.position);
                        print("entered pathtofollow 5 " + pathToFollow);
                        //wait until the path is generated
                        yield return seeker.IsDone();
                        //if the path is different, update the path that I need to follow
                        posns = pathToFollow.vectorPath;
                        print("entered posns 6 " + posns);
                        savePosition();
                        drawTail(enemyLength);
                        yield return new WaitForSeconds(1f);

                        //clearTail();
                    }
                }
                //keep looking for a path because if we have arrived the enemy will anyway move away
                //This code allows us to keep chasing
                pathToFollow = seeker.StartPath(t.position, target.position);
                yield return seeker.IsDone();
                posns = pathToFollow.vectorPath;
                print("end of for " + posns);
            }
            print("before yield");
            yield return null;
            print("after yield");
        }

        /*
        while (true)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= 0.5f && currentTarget != targets.Length)
            {
                targetCounter++;
                currentTarget++;
                target = targets[targetCounter];
            }


            List<Vector3> posns = pathToFollow.vectorPath;
            Debug.Log("Positions Count: " + posns.Count);

            if (this.transform != null)
            {
                for (int counter = 0; counter < posns.Count; counter++)
                {
                    if (posns[counter] != null)
                    {
                        while (Vector3.Distance(t.position, posns[counter]) >= 0.5f)
                        {
                            t.position = Vector3.MoveTowards(t.position, posns[counter], 1f);
                            //since the enemy is moving, I need to make sure that I am following him

                            pathToFollow = seeker.StartPath(t.position, target.transform.position);



                            //wait until the path is generated
                            yield return seeker.IsDone();
                            //if the path is different, update the path that I need to follow
                            posns = pathToFollow.vectorPath;





                            GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
                            yield return new WaitForSeconds(0.5f);
                            drawTail(enemyLength);
                            savePosition();
                        }

                    }

                    if (currentTarget != targets.Length)
                    {
                        //keep looking for a path because if we have arrived the enemy will anyway move away
                        //This code allows us to keep chasing
                        pathToFollow = seeker.StartPath(t.position, target.transform.position);
                        yield return seeker.IsDone();
                        posns = pathToFollow.vectorPath;
                        yield return null;
                    }

                    else if (currentTarget == targets.Length)
                    {
                        targetCounter++;
                        //if target limit reach go to the final target

                        pathToFollow = seeker.StartPath(t.position, target.transform.position);
                        yield return seeker.IsDone();
                        posns = pathToFollow.vectorPath;
                        yield return null;
                    }
                    yield return null;
                }
                yield return null;
            }
            yield return null;
        }

        */



    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "snakeHead")
        {
            SceneManager.LoadScene("GameOver");
        }
    }




    public void drawTail(int length)
    {
        /*
        clearTail();
        if (enemyPastPos.Count > length)
        {
            //the first block behind the player
            int tailStartIndex = enemyPastPos.Count - 1;
            int tailEndIndex = tailStartIndex - length;

            //if length = 4, this should give me the last 4 blocks
            for (int snakeblocks = tailStartIndex; snakeblocks > tailEndIndex; snakeblocks--)
            {
                print(snakeblocks);
                enemyPastPos[snakeblocks].BreadcrumbBox = Instantiate(enemyAI, enemyPastPos[snakeblocks].Position, Quaternion.identity);
                enemyPastPos[snakeblocks].BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.red;
                enemyPastPos[snakeblocks].BreadcrumbBox.transform.SetParent(enemyAI.transform);
            }
        }

        if (firstrun)
        {
            for (int count = length; count > 0; count--)
            {
                enemyPositionRecord fakeBoxPos = new enemyPositionRecord();

                fakeBoxPos.Position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
                enemyPastPos.Add(fakeBoxPos);
            }
            firstrun = false;
            drawTail(enemyLength);
        }
        */
        clearTail();

        if (enemyPastPos.Count > length)
        {
            int tailStartIndex = enemyPastPos.Count - 1;
            int tailEndIndex = tailStartIndex - length;


            //if length = 4, this should give me the last 4 blocks
            for (int snakeblocks = tailStartIndex; snakeblocks > tailEndIndex; snakeblocks--)
            {
                //prints the past position and its order in the list
                //Debug.Log(pastPositions[snakeblocks].Position + " " + pastPositions[snakeblocks].PositionOrder);

                Debug.Log(snakeblocks);

                enemyPastPos[snakeblocks].BreadcrumbBox = Instantiate(breadcrumbBox, enemyPastPos[snakeblocks].Position, Quaternion.identity);
                enemyPastPos[snakeblocks].BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.red;

            }

        }

        if (firstrun)
        {
            for (int count = length; count > 0; count--)
            {
                enemyPositionRecord fakeBoxPos = new enemyPositionRecord();
                float ycoord = count * -1;
                fakeBoxPos.Position = new Vector3(0f, ycoord);
                // Debug.Log(new Vector3(0f, ycoord));
                fakeBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, fakeBoxPos.Position, Quaternion.identity);
                fakeBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.yellow;
                enemyPastPos.Add(fakeBoxPos);
            }
            firstrun = false;
            drawTail(length);
            //Debug.Log("Not long enough yet");
        }

    }


    void clearTail()
    {
        cleanList();
        foreach (enemyPositionRecord p in enemyPastPos)
        {
            Destroy(p.BreadcrumbBox);
        }
    }


    bool boxExists(Vector3 positionToCheck)
    {
        foreach (enemyPositionRecord p in enemyPastPos)
        {
            if (p.Position == positionToCheck)
            {
                if (p.BreadcrumbBox != null)
                {
                    return true;
                }
            }
        }
        return false;
    }


    void savePosition()
    {
        enemyPositionRecord currentBoxPos = new enemyPositionRecord();

        currentBoxPos.Position = this.gameObject.transform.position;
        enemyPos++;
        currentBoxPos.PositionOrder = enemyPos;

        if (!boxExists(this.gameObject.transform.position))
        {
            currentBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, this.transform.position, Quaternion.identity);

            currentBoxPos.BreadcrumbBox.transform.SetParent(pathParent.transform);

            currentBoxPos.BreadcrumbBox.name = enemyPos.ToString();

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        enemyPastPos.Add(currentBoxPos);
    }



    void cleanList()
    {
        for (int counter = enemyPastPos.Count - 1; counter > enemyPastPos.Count; counter--)
        {
            enemyPastPos[counter] = null;
        }
    }



    void SeekerMode(Transform t)
    {
        Vector3[] linePosList;
        if (showPath)
        {
            if (pathToFollow.vectorPath.Count < 2)
            {
                return;
            }
            int i = 1;

            while (i < pathToFollow.vectorPath.Count)
            {
                lr.positionCount = pathToFollow.vectorPath.Count;
                linePosList = pathToFollow.vectorPath.ToArray();
                for (int j = 0; j < linePosList.Length; j++)
                {
                    //renders a line from the player snake's position to the enemy snake's position
                    lr.SetPosition(j, linePosList[j]);
                }
                i++;
            }
        }
        else //disconnect the connection line
        {
            for (int i = pathToFollow.vectorPath.Count - 1; i > 0; i--)
            {
                lr.positionCount = i;
            }
        }
    }
}


/*
IEnumerator MoveTo(Transform pos)
{
    while (true)
    {
        List<Vector3> pstns = pathToFollow.vectorPath;
        for (int count = 0; count < pstns.Count; count++)
        {
            while (Vector3.Distance(pos.position, pstns[count]) >= 1f)
            {
                pos.position = Vector3.MoveTowards(pos.position, pstns[count], 1f);


                pathToFollow = seeker.StartPath(pos.position, target.position);
                yield return seeker.IsDone();
                yield return new WaitForSeconds(0.5f);
            }
        }
        yield return null;
    }
}

IEnumerator thePath()
{
    AstarPath.active.Scan();
    target = GameObject.FindGameObjectWithTag("snakeHead").transform;
    seeker = GetComponent<Seeker>();
    pathToFollow = seeker.StartPath(target.transform.position, target.position);
    yield return seeker.IsDone();
    StartCoroutine(MoveTo(target.transform));
}




    

}
*/






