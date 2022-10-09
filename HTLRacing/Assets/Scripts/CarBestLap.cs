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
        PlayerHighscore = Highscore.LoadHighscore(Player.name, RaceTrackType);
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
            Highscore.SaveHighscore(PlayerHighscore);
            txtBestLap.text = "Best Lap: " + lapTime.ToString("0.00");
        }
        lapTime = 0;
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

    public static Highscore LoadHighscore(string key)
    {
        return JsonUtility.FromJson<Highscore>(PlayerPrefs.GetString(key));
    }

    public static Highscore LoadHighscore(string playerName, raceTrackType raceTrackType)
    {
        Highscore playerHighscore;
        string key = calcPlayerKey(playerName, raceTrackType);

        //Create Highscore, if player started the first time
        if (!PlayerPrefs.HasKey(key))
        {
            playerHighscore = new Highscore();
            playerHighscore.name = playerName;
            playerHighscore.raceTrackType = raceTrackType;

            AllHighscoreKeys.AddHighscoreKey(key);

            PlayerPrefs.SetString(key, JsonUtility.ToJson(playerHighscore));
            PlayerPrefs.Save();
        }

        return JsonUtility.FromJson<Highscore>(PlayerPrefs.GetString(key));
    }


    public static void SaveHighscore(Highscore playerHighscore)
    {
        string key = calcPlayerKey(playerHighscore.name, playerHighscore.raceTrackType);

        PlayerPrefs.SetString(key, JsonUtility.ToJson(playerHighscore));
        PlayerPrefs.Save();
    }

    public static string calcPlayerKey(string playerName, raceTrackType raceTrackType)
    {
        return raceTrackType.ToString() + playerName;
    }
}

[System.Serializable]
public class AllHighscoreKeys
{
    static readonly string keyKey = "allKeys";//yes, i hab des so genannt, mach was dagegen. Kann nix dafür, es is 00:50
    public List<string> keys;

    public AllHighscoreKeys()
    {
        keys = new List<string>();
    }

    public static void AddHighscoreKey(string key)
    {
        AllHighscoreKeys allHighscoreKeys = GetAllHighscoreKeys();
        allHighscoreKeys.keys.Add(key);
        PlayerPrefs.SetString(keyKey, JsonUtility.ToJson(allHighscoreKeys));
        PlayerPrefs.Save();
    }

    public static AllHighscoreKeys GetAllHighscoreKeys()
    {
        AllHighscoreKeys allHighscoreKeys;
        if (!PlayerPrefs.HasKey(keyKey))
        {
            allHighscoreKeys = new AllHighscoreKeys();
            PlayerPrefs.SetString(keyKey, JsonUtility.ToJson(allHighscoreKeys));
            PlayerPrefs.Save();
        }

        allHighscoreKeys = JsonUtility.FromJson<AllHighscoreKeys>(PlayerPrefs.GetString(keyKey));
        return allHighscoreKeys;
    }

}

public enum raceTrackType
{
    track1, track2
}