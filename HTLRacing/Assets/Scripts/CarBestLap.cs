using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class CarBestLap : MonoBehaviour
{
    private static CarBestLap instance;
    public TMP_Text txtBestLap;
    public TMP_Text txtLapTime;
    float lapTime;

    public static Highscore PlayerHighscore = new Highscore();

    private void Start()
    {
        instance = this;
        lapTime = 0;
        StartCoroutine(GetHighscoreCoroutine("localhost:3000/highscore/" + PlayerHighscore.name));       
    }

    private void Update()
    {
        lapTime += Time.deltaTime;
        txtLapTime.text = "Lap Time: " + lapTime.ToString("0.00");
    }

    public static void SetNewBestTimeStatic()
    {
        instance.SetNewBestTime();
    }

    void SetNewBestTime()
    {
        if (lapTime < PlayerHighscore.time)
        {
            PlayerHighscore.time = lapTime;
            txtBestLap.text = "Best Lap: " + lapTime.ToString("0.00");         
            StartCoroutine(SetHighscoreCoroutine("localhost:3000/highscore/" + PlayerHighscore.name));
        }
        lapTime = 0;
    }

    // Use with StartCoroutine !
    public IEnumerator GetHighscoreCoroutine(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Highscore highScore = JsonUtility.FromJson<Highscore>(webRequest.downloadHandler.text);
                txtBestLap.text = "Best Lap: " + highScore.time.ToString("0.00");
                PlayerHighscore.time = highScore.time;
            }
        }
    }

    public IEnumerator SetHighscoreCoroutine(string uri)
    {
        string highscoreData = JsonUtility.ToJson(PlayerHighscore);
        Debug.Log(highscoreData);
        using (UnityWebRequest webRequest = UnityWebRequest.Put(uri, highscoreData))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", "application/json");
            yield return webRequest.SendWebRequest();
        }
    }
}


[System.Serializable]
public class Highscore
{
    public string name;
    public float time;

    public Highscore()
    {
        time = float.MaxValue;
        name = "";
    }
}
