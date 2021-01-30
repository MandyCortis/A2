using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
public class HighScore
{
    public string playername;
    public float playtime;

    public HighScore(string pname, float ptime)
    {
        playername = pname;
        playtime = ptime;
    }
}
*/

public class highScoreController : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    // private List<HighscoreEntry> hsEntryList;
    private List<Transform> hsEntryTransformList;


    private void Awake()
    {
        entryContainer = transform.Find("hsContainer");
        entryTemplate = entryContainer.Find("hsTemplate");

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("HighscorePanel");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            print("Initializing table with default values");
            AddHsEntry(1, "DEFAULT", "1s");

            jsonString = PlayerPrefs.GetString("HighscorePanel");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }
        RefreshHsTable();
    }

    void RefreshHsTable()
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

    private void CreateHsEntryTransform(HighscoreEntry hsEnrty, Transform container, List<Transform> transformList)
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


    
    private class Highscores
    {
        public List<HighscoreEntry> hsEntryList;
    }


    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
        public string time;
    }


    void Start()
    {
        AddHsEntry(GameManager.score, SubmitName.name, timerManager.timerText.text);
        Destroy(GameObject.FindWithTag("name"));
        Destroy(GameObject.FindWithTag("score"));
        Destroy(GameObject.FindWithTag("timer"));
        GameManager.enemyLength = 4;
        GameManager.score = 0;
    }

    void Update()
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

    /*
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        myHighScores = new List<HighScore>();

        myHighScores.Add(new HighScore("Gerry", 65.5f));
        myHighScores.Add(new HighScore("Joey", 60.5f));

    }

    void DisplayList()
    {
        foreach (HighScore s in myHighScores)
        {
            Debug.Log(s.playername + " " + s.playtime);
        }
    }

    void SaveList()
    {
        string[] names = new string[myHighScores.Count];

        float[] playertimes = new float[myHighScores.Count];

        int counter = 0;
        foreach (HighScore s in myHighScores)
        {
            names[counter] = s.playername;
            playertimes[counter] = s.playtime;
            counter++;
        }


        //PlayerPrefsX.SetStringArray("PlayerNames", names);
        //PlayerPrefsX.SetFloatArray("PlayerTimes", playertimes);

    }


    void LoadList()
    {
        string[] names;

        float[] playertimes;

       // names = PlayerPrefsX.GetStringArray("PlayerNames");
       // playertimes = PlayerPrefsX.GetFloatArray("PlayerTimes");


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("Level1");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("Level2");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SaveList();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            LoadList();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayList();
        }
    }
    */


