using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class CarBestLap : MonoBehaviour
{
    public static CarBestLap instance;
    public TMP_Text txtBestLap;
    public TMP_Text txtLapTime;
    float lapTime;

    raceTrackType RaceTrackType;
    Highscore PlayerHighscore;

    public PopUp PopUp;

    public void Initialize(raceTrackType _raceTrackType)
    {
        lapTime = 0;
        RaceTrackType = _raceTrackType;
        LoadHighscore();
        if(PlayerHighscore.time == float.MaxValue)
        {
            txtBestLap.text = "Best Lap: -";
        }
        else
        {
            txtBestLap.text = "Best Lap: " + PlayerHighscore.time.ToString("0.00");
        }    
    }

    private void Start()
    {
        instance = this;         
    }

    private void Update()
    {
        lapTime += Time.deltaTime;
        txtLapTime.text = "Lap Time: " + lapTime.ToString("0.00");

        if(Input.GetKey(KeyCode.A))
        {
            if(Input.GetKeyDown(KeyCode.Delete))
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();
                PopUp.Open("Achtung", "Alle gespeicherten Daten wurden gelöscht. Bitte Spiel neu starten");
            }
        }
    }

    public void SetNewBestTime()
    {
        if (lapTime < PlayerHighscore.time)
        {
            PlayerHighscore.time = lapTime;
            SaveHighscore();
            txtBestLap.text = "Best Lap: " + lapTime.ToString("0.00");
        }
        lapTime = 0;
    }

    void LoadHighscore()
    {
        string key = calcPlayerKey(Player.name, RaceTrackType);

        //Create Highscore, if player started the first time
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerHighscore = new Highscore();
            PlayerHighscore.name = Player.name;
            PlayerHighscore.raceTrackType = RaceTrackType;

            PlayerPrefs.SetString(key, JsonUtility.ToJson(PlayerHighscore));
            PlayerPrefs.Save();
        }

        PlayerHighscore = JsonUtility.FromJson<Highscore>(PlayerPrefs.GetString(key));
    }

    void SaveHighscore()
    {
        string key = calcPlayerKey(Player.name, RaceTrackType);

        PlayerPrefs.SetString(key, JsonUtility.ToJson(PlayerHighscore));
        PlayerPrefs.Save();
    }

    string calcPlayerKey(string playerName, raceTrackType raceTrackType)
    {
        return raceTrackType.ToString() + playerName;
    }
}

[System.Serializable]
public class Highscore
{
    public float time;
    public raceTrackType raceTrackType;
    public string name;

    public Highscore()
    {
        time = float.MaxValue;
        name = string.Empty;
    }
}

public enum raceTrackType
{
    track1, track2
}