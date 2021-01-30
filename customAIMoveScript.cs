using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;
using UnityEngine.SceneManagement;



public class customAIMoveScript : MonoBehaviour
{
    //the object that we are using to generate the path
    Seeker seeker;

    //path to follow stores the path
    Path pathToFollow;

    //a reference to the snakeHead
    Transform target;

    //a reference to PointGraphObject
    GameObject graphParent;




    foodGenerator fgen;
    List<positionRecord> enemyPastPos;
    bool firstrun = true;
    GameObject enemyAI, breadcrumbBox, pathParent;
    int enemyPos = 0;
    int enemyLength = GameManager.enemyLength;

    LineRenderer lineRenderer;
    bool showPath = false;

    void Start()
    {
        //the instance of the seeker attached to this game object
        seeker = GetComponent<Seeker>();

        //setting target's name by tag
        target = GameObject.FindGameObjectWithTag("snakeHead").transform;

        //find the parent node of the grid graph
        graphParent = GameObject.Find("AStarGrid");
        //we scan the graph to generate it in memory
        graphParent.GetComponent<AstarPath>().Scan();

        //generate the initial path
        pathToFollow = seeker.StartPath(transform.position, target.position);

        //update the graph as soon as you can. Runs indefinitely
        StartCoroutine(updateGraph());
        //move the enemy towards the snakeHead
        StartCoroutine(moveTowardsPlayer(this.transform));






        enemyAI = GameObject.FindGameObjectWithTag("enemy");
    }

    private void Update()
    {
        savePosition();

        //draw a tail of length 4
        drawTail(enemyLength);
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
            List<Vector3> posns = pathToFollow.vectorPath;

            for (int counter = 0; counter < posns.Count; counter++)
            {
                if (posns[counter] != null)
                {
                    while (Vector3.Distance(t.position, posns[counter]) >= 1f)
                    {
                        t.position = Vector3.MoveTowards(t.position, posns[counter], 1f);
                        //drawTail(enemyLength);
                        //savePosition();
                        //since the enemy is moving, I need to make sure that I am following him
                        pathToFollow = seeker.StartPath(t.position, target.position);
                        //wait until the path is generated
                        yield return seeker.IsDone();
                        //if the path is different, update the path that I need to follow
                        posns = pathToFollow.vectorPath;

                        yield return new WaitForSeconds(1f);
                    }
                }
                //keep looking for a path because if we have arrived the enemy will anyway move away
                //This code allows us to keep chasing
                pathToFollow = seeker.StartPath(t.position, target.position);
                yield return seeker.IsDone();
                posns = pathToFollow.vectorPath;
            }
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "snakeHead")
        {
            SceneManager.LoadScene("GameOver");
        }
    }




    void drawTail(int length)
    {
        clearTail();
        if (enemyPastPos.Count > length)
        {
            //the first block behind the player
            int tailStartIndex = enemyPastPos.Count - 1;
            int tailEndIndex = tailStartIndex - length;

            //if length = 4, this should give me the last 4 blocks
            for (int snakeblocks = tailStartIndex; snakeblocks > tailEndIndex; snakeblocks--)
            {
                enemyPastPos[snakeblocks].BreadcrumbBox = Instantiate(enemyAI, enemyPastPos[snakeblocks].Position, Quaternion.identity);
                enemyPastPos[snakeblocks].BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }

        if (firstrun)
        {
            for (int count = length; count > 0; count--)
            {
                positionRecord fakeBoxPos = new positionRecord();
                
                fakeBoxPos.Position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
                enemyPastPos.Add(fakeBoxPos);
            }
            firstrun = false;
            drawTail(GameManager.enemyLength);
        }
    }


    void clearTail()
    {
        //cleanList();
        foreach (positionRecord p in enemyPastPos)
        {
            Destroy(p.BreadcrumbBox);
        }
    }


    bool boxExists(Vector3 positionToCheck)
    {
        foreach (positionRecord p in enemyPastPos)
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
        positionRecord currentBoxPos = new positionRecord();

        currentBoxPos.Position = this.gameObject.transform.position;
        enemyPos++;
        currentBoxPos.PositionOrder = enemyPos;

        if (!boxExists(this.gameObject.transform.position))
        {
            currentBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, this.transform.position, Quaternion.identity);

            //currentBoxPos.BreadcrumbBox.transform.SetParent(pathParent.transform);

            //currentBoxPos.BreadcrumbBox.name = enemyPos.ToString();

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
            int x = 1;
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






