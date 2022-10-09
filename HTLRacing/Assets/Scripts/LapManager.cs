using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LapManager : MonoBehaviour
{
    public TMP_Text txtLaps;
    int hittedCheckPoints = 0;


    static List<LapManager> lapmanagers = new List<LapManager>();
    public Lap[] laps;

    private void Start()
    {
        lapmanagers.Add(this);
    }

    public void NextLapHitted()
    {
        hittedCheckPoints++;
        txtLaps.text = "Checkpoint " + hittedCheckPoints + "/" + laps.Length;

        foreach (Lap lap in laps)
        {
            if (lap.hasCollided == false)
            {
                return;
            }
        }
        CarBestLap.SetNewBestTimeStatic();

        foreach (Lap lap in laps)
        {
            lap.hasCollided = false;
            hittedCheckPoints = 0;
            txtLaps.text = "Checkpoint " + hittedCheckPoints + "/" + laps.Length;
        }
    }

    public static void ResetAllLaps()
    {
        foreach(LapManager lapManager in lapmanagers)
        {
            lapManager.ResetLap();
        }
    }

    public void ResetLap()
    {
        foreach (Lap lap in laps)
        {
            lap.hasCollided = false;
        }
    }

    public void Initialize()
    {
        hittedCheckPoints = 0;
        txtLaps.text = "Checkpoint " + hittedCheckPoints + "/" + laps.Length;
    }
}
