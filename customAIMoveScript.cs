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
            Debug.Log("Positions Count: " + posns.Count);

            for (int counter = 0; counter < posns.Count; counter++)
            {
                if (posns[counter] != null)
                {
                    while (Vector3.Distance(t.position, posns[counter]) >= 0.5f)
                    {
                        t.position = Vector3.MoveTowards(t.position, posns[counter], 1f);
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



 IEnumerator Task5()
    {
        //this takes me to the edge of the screen
        float xpos = 0f;
        while (xpos < 10f)
        {
            Debug.Log(xpos);
            enemyBox.transform.position += new Vector3(1f, 0f);
            xpos++;
            savePosition();
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }


    void drawTail(int length)
    {
        clearTail();
        if (pastPositions.Count > length)
        {
            //the first block behind the player
            int tailStartIndex = pastPositions.Count - 1;
            int tailEndIndex = tailStartIndex - length;

            //if length = 4, this should give me the last 4 blocks
            for (int snakeblocks = tailStartIndex; snakeblocks > tailEndIndex; snakeblocks--)
            {
                pastPositions[snakeblocks].BreadcrumbBox = Instantiate(breadcrumbBox, pastPositions[snakeblocks].Position, Quaternion.identity);
            }
        }

        if (firstrun)
        {
            for (int count = length; count > 0; count--)
            {
                positionRecord fakeBoxPos = new positionRecord();
                float ycoord = count * -1;
                fakeBoxPos.Position = new Vector3(0f, ycoord);
                pastPositions.Add(fakeBoxPos);
            }
            firstrun = false;
            drawTail(length);
        }
    }


    void clearTail()
    {
        //cleanList();
        foreach (positionRecord p in pastPositions)
        {
            Destroy(p.BreadcrumbBox);
        }
    }


    bool boxExists(Vector3 positionToCheck)
    {
        foreach (positionRecord p in pastPositions)
        {
            if (p.Position == positionToCheck)
            {
                Debug.Log(p.Position + "Actually was a past position");
                if (p.BreadcrumbBox != null)
                {
                    Debug.Log(p.Position + "Actually has a red box already");
                    return true;
                }
            }
        }
        return false;
    }


    void savePosition()
    {
        positionRecord currentBoxPos = new positionRecord();

        currentBoxPos.Position = enemyBox.transform.position;
        positionorder++;
        currentBoxPos.PositionOrder = positionorder;

        if (!boxExists(enemyBox.transform.position))
        {
            currentBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, enemyBox.transform.position, Quaternion.identity);

            currentBoxPos.BreadcrumbBox.transform.SetParent(pathParent.transform);

            currentBoxPos.BreadcrumbBox.name = positionorder.ToString();

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.red;

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        pastPositions.Add(currentBoxPos);
        Debug.Log("Have made this many moves: " + pastPositions.Count);

    }



    void cleanList()
    {
        for (int counter = pastPositions.Count - 1; counter > pastPositions.Count; counter--)
        {
            pastPositions[counter] = null;
        }
    }

}
*/






