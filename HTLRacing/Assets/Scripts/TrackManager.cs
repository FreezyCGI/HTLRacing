using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public LapManager LapManager;
    public raceTrackType raceTrackType;

    public void InitializeTrack()
    {
        LapManager.Initialize();
        CarBestLap.instance.Initialize(raceTrackType);
    }
}
