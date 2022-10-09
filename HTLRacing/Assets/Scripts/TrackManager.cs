using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public LapManager LapManager;
    public raceTrackType raceTrackType;

    List<GameObject> highscoreUIList = new List<GameObject>();

    public GameObject highScoreContentTemplate;
    GameObject highScoreContent;

    private void Start()
    {
        highScoreContent = highScoreContentTemplate.transform.parent.gameObject;
        highScoreContentTemplate.SetActive(false);
    }

    public void InitializeTrack()
    {
        LapManager.Initialize();
        CarBestLap.instance.Initialize(raceTrackType);
        LoadHighscores();
    }

    void LoadHighscores()
    {
        ClearHighscores();
        AllHighscoreKeys allHighscoreKeys = AllHighscoreKeys.GetAllHighscoreKeys();
        foreach(string key in allHighscoreKeys.keys)
        {
            Highscore playerHighscore = Highscore.LoadHighscore(key);

            GameObject highScoreGameObject = Instantiate(highScoreContentTemplate);
            highScoreGameObject.transform.SetParent(highScoreContent.transform);
            HighscoreUI highscoreUI = highScoreGameObject.GetComponent<HighscoreUI>();
            highscoreUI.SetHighscore(playerHighscore);

            highscoreUIList.Add(highScoreGameObject);
        }
    }

    void ClearHighscores()
    {
        for(int i = 0; i< highscoreUIList.Count; i++)
        {
            Destroy(highscoreUIList[i]);
        }
        highscoreUIList.Clear();
    }
}
