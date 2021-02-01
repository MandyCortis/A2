using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GameData
{
    public Text txtName;
    public string Pname;

    public float time;
    public GameData(string nameStr, float timeF)
    {
        Pname = nameStr;
        time = timeF;
    }
}
