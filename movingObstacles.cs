﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingObstacles : MonoBehaviour
{
    public List<Transform> waypoints1, waypoints2, waypoints3;

    public Transform obs1, obs2, obs3;

    int waypointsChildren;


    IEnumerator moveObs1()
    {
        for (int i = waypointsChildren; i >= 0; i++)
        {
            foreach (Transform mytransform in waypoints1)
            {
                while (Vector3.Distance(obs1.position, mytransform.position) > 1f)
                {
                    //1 unit towards the first one
                    obs1.position = Vector3.MoveTowards(obs1.position, mytransform.position, 1f);

                    yield return new WaitForSeconds(1f);
                }
                yield return null;
            }
            yield return null;
        }
        
    }

    IEnumerator moveObs2()
    {
        for (int i = waypointsChildren; i >= 0; i++)
        {
            foreach (Transform mytransform in waypoints2)
            {
                while (Vector3.Distance(obs2.position, mytransform.position) > 1f)
                {
                    //1 unit towards the first one
                    obs2.position = Vector3.MoveTowards(obs2.position, mytransform.position, 1f);

                    yield return new WaitForSeconds(1f);
                }
                yield return null;
            }
            yield return null;
        }
    }


    IEnumerator moveObs3()
    {
        for (int i = waypointsChildren; i >= 0; i++)
        {
            foreach (Transform mytransform in waypoints3)
            {
                while (Vector3.Distance(obs3.position, mytransform.position) > 1f)
                {
                    //1 unit towards the first one
                    obs3.position = Vector3.MoveTowards(obs3.position, mytransform.position, 1f);

                    yield return new WaitForSeconds(1f);
                }
                yield return null;
            }
            yield return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        waypointsChildren = GameObject.Find("Waypoints").transform.childCount; 
        StartCoroutine(moveObs1());
        StartCoroutine(moveObs2());
        StartCoroutine(moveObs3());
    }

    private void Update()
    {
        AstarPath.active.Scan();
    }
}
