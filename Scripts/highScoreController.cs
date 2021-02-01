using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class highScoreController : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private List<Transform> hsEntryTransformList;


    public void Awake()
    {
        entryContainer = transform.Find("hsContainer");
        entryTemplate = entryContainer.Find("hsTemplate");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("HighscorePanel");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            AddHsEntry("Null", "59.59s", 59.59f);
            jsonString = PlayerPrefs.GetString("HighscorePanel");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }
        RefreshHsTable();
    }


    public void RefreshHsTable()
    {
        string jsonString = PlayerPrefs.GetString("HighscorePanel");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


        highscores.Sort();


        if (hsEntryTransformList != null)
        {
            foreach (Transform hsEntryTransform in hsEntryTransformList)
            {
                Destroy(hsEntryTransform.gameObject);
            }
        }

        hsEntryTransformList = new List<Transform>();

        foreach (HighscoreEntry hsEntry in highscores.hsEntryList)
        {
            CreateHsEntryTransform(hsEntry, entryContainer, hsEntryTransformList);
        }
    }

    public void CreateHsEntryTransform(HighscoreEntry hsEnrty, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("posEntry").GetComponent<Text>().text = rankString;

        string name = hsEnrty.name;
        entryTransform.Find("nameEntry").GetComponent<Text>().text = name;

        string time = hsEnrty.time;
        entryTransform.Find("timeEntry").GetComponent<Text>().text = time;

        if (rank == 1)
        {
            entryTransform.Find("posEntry").GetComponent<Text>().color = Color.green;
            entryTransform.Find("nameEntry").GetComponent<Text>().color = Color.green;
            entryTransform.Find("timeEntry").GetComponent<Text>().color = Color.green;
        }

        transformList.Add(entryTransform);

    }

    public void AddHsEntry(string name, string time, float hsTime)
    {
        //Create HighscoreEntry
        HighscoreEntry hsEntry = new HighscoreEntry { name = name, time = time, hsTime = hsTime};

        //Load saved Highscores
        string jsonString = PlayerPrefs.GetString("HighscorePanel");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            highscores = new Highscores()
            {
                hsEntryList = new List<HighscoreEntry>()
            };
        }

        //Add new entry to Highscores
        highscores.hsEntryList.Add(hsEntry);

        //Save updates Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("HighscorePanel", json);
        PlayerPrefs.Save();

        RefreshHsTable();
    }


    
    public class Highscores
    {
        public List<HighscoreEntry> hsEntryList;
        public void Sort()
        {
            hsEntryList.Sort();
        }
    }


    [System.Serializable]
    public class HighscoreEntry: System.IComparable<HighscoreEntry>
    {
        
        public string name;
        public string time;
        public float hsTime;
        

        int IComparable<HighscoreEntry>.CompareTo(HighscoreEntry other)
        {
            return Convert.ToInt32(hsTime - other.hsTime);
        }
    }


    public void Start()
    {
        AddHsEntry(GameManager.Pname, timerManager.timerText.text, timerManager.timerValue);
        //Destroy(GameObject.FindWithTag("name"));
        //Destroy(GameObject.FindWithTag("timer"));
    }

    public void Update()
    {
        {
            if(Input.GetKeyDown(KeyCode.Delete))
            {
                PlayerPrefs.DeleteAll();
                print("deleted");
                RefreshHsTable();
            }
        }
    }
}


