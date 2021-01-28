using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTransform : MonoBehaviour
{
    foodGenerator fgen;
    private float timer = 0.0f;
    private float waitTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        fgen = Camera.main.GetComponent<foodGenerator>();
        timer += Time.deltaTime;
    }

    /*
    // Update is called once per frame
    void Update()
    {
        if (timer > waitTime)
        {
            //GameObject.Find("foodObject").GetComponent<foodGenerator>().fgen = gameObject;

            int randomObjects = Random.Range(0, positionRecord.Length); //list of gameObjects to spawn
            int randomSpawn = Random.Range(0, Spawners.Length); //list of empty gameObjects

            if (objectList.Count < 5)
            {
                yield return new WaitForSecondsRealtime(0f);
                GameObject object = Instantiate(objects[randomObjects], transform.position, transform.rotation);
                objectList.Add(object);
            }
        }
    }
    */


}
