using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
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


        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;
            enemyPositionRecord p = obj as enemyPositionRecord;
            if ((System.Object)p == null)
                return false;
            return position == p.position;
        }


        public bool Equals(enemyPositionRecord o)
        {
            if (o == null)
                return false;

            //the distance between any food spawned
            return Vector3.Distance(this.position, o.position) < 4f;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public Vector3 Position { get => position; set => position = value; }
        public int PositionOrder { get => positionOrder; set => positionOrder = value; }
        public GameObject BreadcrumbBox { get => breadcrumbBox; set => breadcrumbBox = value; }
    }
   
}
