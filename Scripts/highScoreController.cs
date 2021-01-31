using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class highScoreController : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    // private List<HighscoreEntry> hsEntryList;
    private List<Transform> hsEntryTransformList;

    GameManager gm;


    public void Awake()
    {
        entryContainer = transform.Find("hsContainer");
        entryTemplate = entryContainer.Find("hsTemplate");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("HighscorePanel");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            print("Initializing table with default values");
            //AddHsEntry(1, "DEFAULT", "1s");

            jsonString = PlayerPrefs.GetString("HighscorePanel");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }
        RefreshHsTable();
    }

    public void RefreshHsTable()
    {
        string jsonString = PlayerPrefs.GetString("HighscorePanel");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


        //Sort entry list by score
        for (int i = 0; i < highscores.hsEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.hsEntryList.Count; j++)
            {
                if (highscores.hsEntryList[j].score > highscores.hsEntryList[i].score)
                {
                    //swap
                    HighscoreEntry temp = highscores.hsEntryList[i];
                    highscores.hsEntryList[i] = highscores.hsEntryList[j];
                    highscores.hsEntryList[j] = temp;
                }
            }
        }

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

        int score = hsEnrty.score;

        entryTransform.Find("scoreEntry").GetComponent<Text>().text = score.ToString();

        string name = hsEnrty.name;
        entryTransform.Find("nameEntry").GetComponent<Text>().text = name;

        string time = hsEnrty.time;
        entryTransform.Find("timeEntry").GetComponent<Text>().text = time;

        if (rank == 1)
        {
            entryTransform.Find("posEntry").GetComponent<Text>().color = Color.green;
            entryTransform.Find("scoreEntry").GetComponent<Text>().color = Color.green;
            entryTransform.Find("nameEntry").GetComponent<Text>().color = Color.green;
        }

        transformList.Add(entryTransform);

    }

    public void AddHsEntry(int score, string name, string time)
    {
        //Create HighscoreEntry
        HighscoreEntry hsEntry = new HighscoreEntry { score = score, name = name, time = time };

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
    }


    [System.Serializable]
    public class HighscoreEntry
    {
        public int score;
        public string name;
        public string time;
    }


    public void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        print("entered hs");
        //AddHsEntry(GameManager.score, GameManager.Pname, timerManager.timerText.text);
        //Destroy(GameObject.FindWithTag("name"));
        //Destroy(GameObject.FindWithTag("score"));
        //Destroy(GameObject.FindWithTag("timer"));
        //GameManager.enemyTailLength = 4;
        //GameManager.score = 0;
    }

    public void Update()
    {
        {
            if(Input.GetKeyDown(KeyCode.Delete))
            {
                PlayerPrefs.DeleteAll();
                print("deleted");
            }
        }
    }
}


